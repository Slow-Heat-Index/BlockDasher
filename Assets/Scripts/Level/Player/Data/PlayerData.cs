using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Level.Cameras.Behaviour;
using Level.Generator;
using Sources.Level;
using Sources.Level.Blocks;
using Sources.Util;
using UnityEngine;

namespace Level.Player.Data {
    public class PlayerData : MonoBehaviour {
        private static readonly int AnimatorMove = Animator.StringToHash("Move");
        private static readonly int AnimatorMove2 = Animator.StringToHash("Move2");
        private static readonly int AnimatorJump = Animator.StringToHash("Jump");
        private static readonly int AnimatorBump = Animator.StringToHash("Bump");
        private static readonly int AnimatorFall = Animator.StringToHash("Fall");
        private static readonly int AnimatorDeath = Animator.StringToHash("Death");
        private static readonly int AnimatorDrown = Animator.StringToHash("Drown");
        private static readonly int AnimatorIdle = Animator.StringToHash("Idle");
        public event Action onWin;
        public event Action onPreLose;
        public event Action onLose;
        public event Action onReset;

        public event Action onMove;

        public int extraSteps = 0;
        public uint movements = 0;
        public bool hasWon;
        public bool dead;


        public bool executingDeathAnimation;
        public bool shouldCameraFollow = true;

        public int movementsOnQuicksand = 0;
        public int movementsInWater = 0;

        public int maxMovementsOnQuicksand = 3;
        public int maxMovementsInWater = 4;

        public bool nextMoveJump;

        [Header("Animation")] public float movementSpeed = 0.08f;

        private Animator _animator;
        private LevelGenerator _level;
        private readonly Queue<Vector3> _movementQueue = new Queue<Vector3>();
        private Tween _movementTween;
        private PlayerSoundManager _playerSoundManager;

        private LevelCameraBehaviour _cameraBehaviour;

        private MovesCounter _movesCounter;

        private StatusManager _statusManager;

        public bool CanPlayerMove =>
            _movementQueue.Count == 0 &&
            (_movementTween == null || !_movementTween.IsActive() || _movementTween.IsComplete());

        public BlockPosition BlockPosition { get; private set; }


        private void Awake() {
            _level = FindObjectOfType<LevelGenerator>();
            _animator = GetComponentInChildren<Animator>();
            _cameraBehaviour = FindObjectOfType<LevelCameraBehaviour>();
            _playerSoundManager = GetComponent<PlayerSoundManager>();
            _movesCounter = FindObjectOfType<MovesCounter>();
            _statusManager = FindObjectOfType<StatusManager>();

            BlockPosition = _level.World.StartPosition.Position;

            UpdateTransform();
        }

        private void Update() {
            if (_movementQueue.Count <= 0) return;
            var waypoints = _movementQueue.ToArray();

            for (var i = 0; i < waypoints.Length; i++) {
                waypoints[i] += new Vector3(0, -movementsOnQuicksand / (float)(maxMovementsOnQuicksand + 1), 0);
            }

            _movementQueue.Clear();

            if (nextMoveJump) {
                nextMoveJump = false;
                _movementTween = transform.DOPath(waypoints, 0.3f);
                _animator.Play(AnimatorJump, 0);
            }
            else {
                var vertical = waypoints[0].y - transform.position.y != 0;
                if (!vertical) {
                    for (var i = 1; i < waypoints.Length; i++) {
                        if (waypoints[i - 1].y - waypoints[i].y == 0) continue;
                        vertical = true;
                        break;
                    }
                }

                _movementTween = transform.DOPath(waypoints, movementSpeed * waypoints.Length);
                if (!vertical) {
                    if (waypoints.Length > 1) {
                        SetAnimation(AnimatorMove2, AnimatorMove);
                        _movementTween.onComplete = () => SetAnimation(AnimatorMove, AnimatorMove2);
                    }
                    else if (waypoints.Length > 0) {
                        _animator.Play(AnimatorBump, 0);
                        _playerSoundManager.PlayRandomPitch(3);
                    }
                }
                else {
                    _animator.Play(AnimatorFall, 0);
                    _movementTween.onComplete = () => _animator.Play(AnimatorIdle, 0);
                }
            }
        }

        public void Move(Vector3Int offset) {
            BlockPosition = BlockPosition.Moved(offset);
            _movementQueue.Enqueue(BlockPosition.Position + new Vector3(0.5f, 0, 0.5f));
            CheckCurrentBlockOnMove();
        }

        public void Teleport(Vector3Int position) {
            BlockPosition = new BlockPosition(BlockPosition.World, position);
            _movementQueue.Clear();
            if (_movementTween != null && _movementTween.IsActive() && !_movementTween.IsComplete()) {
                _movementTween.Kill(true);
            }

            UpdateTransform();
            CheckCurrentBlockOnMove();
        }

        public void FinishMoving() {
            onMove.Invoke();
            
            var current = BlockPosition.Block;
            var down = BlockPosition.Moved(Direction.Down).Block;
            if (current == null || current.CanMoveTo(Direction.Down)) {
                if (down == null || down.CanMoveFrom(Direction.Up)) {
                    // OWO PLAYER IS DEAD
                    Lose(PlayerDeathCause.FALL);
                    return;
                }
            }

            if (down is QuicksandBlock) {
                movementsOnQuicksand++;
                if (movementsOnQuicksand > maxMovementsOnQuicksand) {
                    Lose(PlayerDeathCause.DROWN);
                    _statusManager.ResetSand();
                    return;
                }

                _statusManager.SetSand(movementsOnQuicksand - 1);

                _playerSoundManager.PlayRandomPitch(2);
            }
            else {
                movementsOnQuicksand = 0;
                _statusManager.ResetSand();
                _playerSoundManager.PlayRandomPitch(0);
            }

            if (current is WaterBlock) {
                movementsInWater++;
                if (movementsInWater > maxMovementsInWater) {
                    Lose(PlayerDeathCause.DROWN);

                    _statusManager.ResetWater();
                    return;
                }

                _statusManager.SetWater(movementsInWater - 1);

                _playerSoundManager.PlayRandomPitch(4);
            }

            else {
                movementsInWater = 0;
                _statusManager.ResetWater();
                _playerSoundManager.PlayRandomPitch(0);
            }

            if (shouldCameraFollow) {
                _cameraBehaviour.UpdateCameraPosition();
            }

            movements++;
            _movesCounter.AddMovement(movements);
        }

        public void Win() {
            hasWon = true;
            onWin?.Invoke();
        }

        public void Lose(PlayerDeathCause cause) {
            if (dead) return;
            executingDeathAnimation = true;
            shouldCameraFollow = cause != PlayerDeathCause.FALL;
            dead = true;

            var waypoints = _movementQueue.ToArray();

            for (var i = 0; i < waypoints.Length; i++) {
                waypoints[i] += new Vector3(0, -(movementsOnQuicksand - 1) / (float)(maxMovementsOnQuicksand + 1), 0);
            }

            switch (cause) {
                case PlayerDeathCause.FALL: {
                    _animator.Play(AnimatorFall, 0);
                    _playerSoundManager.Play(1);
                    for (var i = 0; i < 20; i++) {
                        ref var v = ref waypoints[waypoints.Length - 1 - i];
                        v.y -= 1.2f * i;
                    }

                    break;
                }
                case PlayerDeathCause.DROWN:
                    _animator.Play(AnimatorDrown, 0);
                    break;
                default:
                    // PUNCH
                    _animator.Play(AnimatorDeath, 0);
                    _playerSoundManager.Play(5);
                    break;
            }

            _movementQueue.Clear();
            _movementTween = transform.DOPath(waypoints, movementSpeed * waypoints.Length)
                .SetEase(Ease.Linear);
            _movementTween.onComplete = () => { StartCoroutine(DeathAnimationCompleted(cause)); };

            onPreLose?.Invoke();
        }

        public void Reset() {
            Teleport(BlockPosition.World.StartPosition.Position.Position);
            movements = 0;
            movementsOnQuicksand = 0;
            movementsInWater = 0;
            dead = false;
            hasWon = false;
            _cameraBehaviour.TeleportCamera();
            onReset?.Invoke();

            _animator.Play(AnimatorIdle, 0);
            shouldCameraFollow = true;
            _level.World.ResetLevel(true);
            
            _statusManager.ResetWater();
            _statusManager.ResetSand();
            
            _movesCounter.AddMovement(movements);
        }

        private void UpdateTransform() {
            transform.position = BlockPosition.Position + new Vector3(0.5f, 0, 0.5f);
        }

        private void CheckCurrentBlockOnMove() {
            var current = BlockPosition.Block;
            var down = BlockPosition.Moved(Direction.Down).Block;
            if (!(current is WaterBlock)) {
                movementsInWater = 0;
            }

            if (!(down is QuicksandBlock)) {
                movementsOnQuicksand = 0;
            }
        }


        private void SetAnimation(int from, int to) {
            var info = _animator.GetCurrentAnimatorStateInfo(0);
            if (info.shortNameHash == from) {
                _animator.Play(to, 0, 1 - Math.Min(1, info.normalizedTime));
            }
            else {
                _animator.Play(to, 0);
            }
        }

        private IEnumerator DeathAnimationCompleted(PlayerDeathCause cause) {
            switch (cause) {
                case PlayerDeathCause.PUNCH:
                    yield return new WaitForSeconds(2);
                    break;
                case PlayerDeathCause.DROWN:
                    yield return new WaitForSeconds(1);
                    break;
            }

            onLose?.Invoke();
            /*
            Reset();*/
        }
    }
}