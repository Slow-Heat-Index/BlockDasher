using DG.Tweening;
using Level.Player.Data;
using Sources.Util;
using UnityEngine;

namespace Level.Cameras.Behaviour {
    public class LevelCameraBehaviour : MonoBehaviour {
        public Direction direction = Direction.North;
        [SerializeField] private float distance = 5;
        [SerializeField] private float animationDuration = 0.5f;
        private Tween _moveTween, _rotateTween;


        private PlayerData _player;

        private void Start() {
            _player = FindObjectOfType<PlayerData>();
            TeleportCamera();
        }

        public void RotateRight() {
            if(!_player.shouldCameraFollow) return;
            direction = direction.Rotated(Direction.West);
            UpdateCameraPosition();
        }

        public void RotateLeft() {
            if(!_player.shouldCameraFollow) return;
            direction = direction.Rotated(Direction.East);
            UpdateCameraPosition();
        }

        public void UpdateCameraPosition() {
            _moveTween?.Kill();
            _rotateTween?.Kill();

            var target = _player.BlockPosition.Position + new Vector3(0.5f, 0.5f, 0.5f);
            var offset = (direction.GetVector() - new Vector3(0, 1, 0)).normalized * distance;
            _moveTween = transform.DOMove(target - offset, animationDuration);
            _rotateTween = transform.DODynamicLookAt(target + offset, animationDuration);
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