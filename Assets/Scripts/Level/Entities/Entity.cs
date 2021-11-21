using System.Collections.Generic;
using DG.Tweening;
using Level.Player.Behaviour;
using Level.Player.Data;
using Sources.Level;
using UnityEngine;

namespace Level.Entities {
    public class Entity : MonoBehaviour {
        [Header("Animation")] public float movementSpeed = 0.08f;

        private readonly Queue<Vector3> _movementQueue = new Queue<Vector3>();
        protected Tween _movementTween;

        public BlockPosition Position { get; private set; }

        protected virtual void Start() {
        }

        protected virtual void Update() {
            if (_movementQueue.Count <= 0) return;
            var waypoints = _movementQueue.ToArray();
            _movementQueue.Clear();
            _movementTween = transform.DOPath(waypoints, movementSpeed * waypoints.Length);
            _movementTween.onComplete = OnTweenComplete;
        }

        public virtual void BeforeDash(PlayerData player) {
        }

        public virtual void AfterMove(DashData dashData) {
        }


        public virtual void AfterDash(DashData dashData) {
        }

        public virtual void AfterFall(DashData dashData) {
        }

        public virtual void OnTweenComplete() {
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