﻿using System;
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
        public event Action onWin;
        public event Action onReset;

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

        [Header("Animation")] public float movementSpeed = 0.08f;

        private Animator _animator;
        private LevelGenerator _level;
        private readonly Queue<Vector3> _movementQueue = new Queue<Vector3>();
        private Tween _movementTween;

        private LevelCameraBehaviour _cameraBehaviour;

        public bool CanPlayerMove =>
            _movementQueue.Count == 0 &&
            (_movementTween == null || !_movementTween.IsActive() || _movementTween.IsComplete());

        public BlockPosition BlockPosition { get; private set; }


        private void Awake() {
            _level = FindObjectOfType<LevelGenerator>();
            _animator = GetComponentInChildren<Animator>();
            _cameraBehaviour = FindObjectOfType<LevelCameraBehaviour>();
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
            _movementTween = transform.DOPath(waypoints, movementSpeed * waypoints.Length);
            _movementTween.onComplete = () => SetAnimation(AnimatorMove, AnimatorMove2);

            var vertical = waypoints[0].y - transform.position.y != 0;
            if (!vertical) {
                for (var i = 1; i < waypoints.Length; i++) {
                    if (waypoints[i - 1].y - waypoints[i].y == 0) continue;
                    vertical = true;
                    break;
                }
            }

            if (waypoints.Length > 1 && !vertical) {
                SetAnimation(AnimatorMove2, AnimatorMove);
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
            var current = BlockPosition.Block;
            var down = BlockPosition.Moved(Direction.Down).Block;
            if (current == null || current.CanMoveTo(Direction.Down)) {
                if (down == null || down.CanMoveFrom(Direction.Up)) {
                    // OWO PLAYER IS DEAD
                    Lose(true);
                    return;
                }
            }

            if (down is QuicksandBlock) {
                movementsOnQuicksand++;
                if (movementsOnQuicksand > maxMovementsOnQuicksand) {
                    Lose(false);
                    return;
                }
            }
            else {
                movementsOnQuicksand = 0;
            }

            if (current is WaterBlock) {
                movementsInWater++;
                if (movementsInWater > maxMovementsInWater) {
                    Lose(false);
                    return;
                }
            }
            else {
                movementsInWater = 0;
            }

            if (shouldCameraFollow) {
                _cameraBehaviour.UpdateCameraPosition();
            }

            movements++;
        }

        public void Win() {
            hasWon = true;
            onWin?.Invoke();
        }

        public void Lose(bool fall) {
            if (dead) return;
            executingDeathAnimation = true;
            shouldCameraFollow = !fall;
            dead = true;

            var waypoints = _movementQueue.ToArray();

            if (fall) {
                for (var i = 0; i < 20; i++) {
                    ref var v = ref waypoints[waypoints.Length - 1 - i];
                    v.y -= 1.2f * i;
                }
            }

            _movementQueue.Clear();
            _movementTween = transform.DOPath(waypoints, movementSpeed * waypoints.Length)
                .SetEase(Ease.Linear);
            _movementTween.onComplete = () => {
                shouldCameraFollow = true;
                _level.World.ResetLevel(true);
                Reset();
            };
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
    }
}