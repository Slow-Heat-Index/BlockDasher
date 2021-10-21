using System.Collections.Generic;
using DG.Tweening;
using Level.Generator;
using Sources.Level;
using UnityEngine;

namespace Level.Player.Data {
    public class PlayerData : MonoBehaviour {
        public int extraSteps = 0;
        public uint movementsLeft = 0;
        public bool hasWon;

        [Header("Animation")] public float movementSpeed = 0.08f;

        private LevelGenerator _level;
        private readonly Queue<Vector3> _movementQueue = new Queue<Vector3>();
        private Tween _movementTween;

        public bool CanPlayerMove =>
            _movementQueue.Count == 0 &&
            (_movementTween == null || !_movementTween.IsActive() || _movementTween.IsComplete());

        public BlockPosition BlockPosition { get; private set; }


        private void Awake() {
            _level = FindObjectOfType<LevelGenerator>();
            movementsLeft = _level.World.InitialMoves;
            BlockPosition = _level.World.StartPosition.Position;
            UpdateTransform();
        }

        private void Update() {
            if (_movementQueue.Count <= 0) return;
            var waypoints = _movementQueue.ToArray();
            _movementQueue.Clear();
            _movementTween = transform.DOPath(waypoints, movementSpeed * waypoints.Length);
        }

        public void Move(Vector3Int offset) {
            BlockPosition = BlockPosition.Moved(offset);
            _movementQueue.Enqueue(BlockPosition.Position + new Vector3(0.5f, 0.5f, 0.5f));
        }

        public void Teleport(Vector3Int position) {
            BlockPosition = new BlockPosition(BlockPosition.World, position);
            _movementQueue.Clear();
            if (_movementTween != null && _movementTween.IsActive() && !_movementTween.IsComplete()) {
                _movementTween.Kill(true);
            }

            UpdateTransform();
        }

        public void Win() {
            hasWon = true;
        }

        public void Lose() {
            Teleport(BlockPosition.World.StartPosition.Position.Position);
            movementsLeft = BlockPosition.World.InitialMoves;
            _level.World.ResetLevel();
        }

        private void UpdateTransform() {
            transform.position = BlockPosition.Position + new Vector3(0.5f, 0.5f, 0.5f);
        }
    }
}