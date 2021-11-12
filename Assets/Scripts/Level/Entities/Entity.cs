using System.Collections.Generic;
using DG.Tweening;
using Sources.Level;
using UnityEngine;

namespace Level.Entities {
    public class Entity : MonoBehaviour {
        [Header("Animation")] public float movementSpeed = 0.08f;

        private readonly Queue<Vector3> _movementQueue = new Queue<Vector3>();
        private Tween _movementTween;

        public BlockPosition Position { get; private set; }

        public virtual void Update() {
            if (_movementQueue.Count <= 0) return;
            var waypoints = _movementQueue.ToArray();
            _movementQueue.Clear();
            _movementTween = transform.DOPath(waypoints, movementSpeed * waypoints.Length);
        }

        public virtual void BeforeDash() {
        }

        public virtual void AfterDash() {
        }

        public virtual void AfterFall() {
        }

        public void Move(Vector3Int offset) {
            Position = Position.Moved(offset);
            _movementQueue.Enqueue(Position.Position + new Vector3(0.5f, 0, 0.5f));
        }

        public void InitPosition(Vector3Int position, World world) {
            Position = new BlockPosition(world, position);
            _movementQueue.Clear();
            if (_movementTween != null && _movementTween.IsActive() && !_movementTween.IsComplete()) {
                _movementTween.Kill(true);
            }

            UpdateTransform();
        }

        private void UpdateTransform() {
            transform.position = Position.Position + new Vector3(0.5f, 0, 0.5f);
        }
    }
}