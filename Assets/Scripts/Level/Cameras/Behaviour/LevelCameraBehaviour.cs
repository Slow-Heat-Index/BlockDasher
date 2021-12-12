using System;
using DG.Tweening;
using Level.Player.Data;
using Sources.Level.Raycast;
using Sources.Util;
using UnityEngine;
using Util;

namespace Level.Cameras.Behaviour {
    public class LevelCameraBehaviour : MonoBehaviour {
        public const float MovementDelay = 0.2f;

        public Direction direction = Direction.North;
        [SerializeField] private float distance = 5;
        [SerializeField] private float animationDuration = 0.5f;
        private Tween _moveTween, _rotateTween;
        private float _lastMovement;


        private PlayerData _player;

        private void Start() {
            _player = FindObjectOfType<PlayerData>();
            TeleportCamera();
        }

        public void RotateRight() {
            if (!_player.shouldCameraFollow || _lastMovement + MovementDelay > Time.time) return;
            direction = direction.Rotated(Direction.West);
            UpdateCameraPosition();
            _lastMovement = Time.time;
        }

        public void RotateLeft() {
            if (!_player.shouldCameraFollow || _lastMovement + MovementDelay > Time.time) return;
            direction = direction.Rotated(Direction.East);
            UpdateCameraPosition();
            _lastMovement = Time.time;
        }

        public void UpdateCameraPosition() {
            const float maxChecks = 5;

            _moveTween?.Kill();
            _rotateTween?.Kill();

            var target = _player.BlockPosition.Position + new Vector3(0.5f, 0.5f, 0.5f);
            var offset = (direction.GetVector() - new Vector3(0, 1, 0)).normalized * distance;

            var origin = target - offset;
            var targetDirection = target + offset;

            var finalOrigin = origin;
            for (var i = 0; i < maxChecks; i++) {
                var position = origin - offset * i;
                var blockPos = position.toInt();
                var block = _player.BlockPosition.World.GetBlock(blockPos);
                if (block != null && block.CollisionBox.Collides(position, blockPos.toFloat())) continue;
                
                if (i == 0) break;
                var raycast = new BlockRaycaster(_player.BlockPosition.World, position, offset, distance);
                raycast.Run();
                if (raycast.Result != null) {
                    var collisionDistance = (float)Math.Sqrt(raycast.CurrentDistanceSquared) * 0.8f;
                    finalOrigin = position + offset * collisionDistance / distance;
                }
                else {
                    // Should not happen!
                    finalOrigin = position;
                }

                break;
            }

            _moveTween = transform.DOMove(finalOrigin, animationDuration);
            _rotateTween = transform.DODynamicLookAt(targetDirection, animationDuration);
        }

        public void TeleportCamera() {
            _moveTween?.Kill();
            _rotateTween?.Kill();

            var target = _player.BlockPosition.Position + new Vector3(0.5f, 0.5f, 0.5f);
            var offset = (direction.GetVector() - new Vector3(0, 1, 0)).normalized * distance;

            transform.position = target - offset;
            transform.LookAt(target + offset);
        }
    }
}