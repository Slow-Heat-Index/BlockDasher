using DG.Tweening;
using Level.Player.Data;
using Sources.Util;
using UnityEngine;

namespace Level.Cameras.Behaviour {
    public class LevelCameraBehaviour : MonoBehaviour {
        public Direction direction = Direction.North;
        [SerializeField] private PlayerData player;
        [SerializeField] private float distance = 5;
        [SerializeField] private float animationDuration = 0.5f;
        private Tween _moveTween, _rotateTween;

        private void Start() {
            UpdateCameraPosition();
        }

        public void RotateRight() {
            direction = direction.Rotated(Direction.West);
            UpdateCameraPosition();
        }

        public void RotateLeft() {
            direction = direction.Rotated(Direction.East);
            UpdateCameraPosition();
        }

        public void UpdateCameraPosition() {
            _moveTween?.Kill();
            _rotateTween?.Kill();

            var target = player.BlockPosition.Position + new Vector3(0.5f, 0.5f, 0.5f);
            var offset = (direction.GetVector() - new Vector3(0, 2, 0)).normalized * distance;
            _moveTween = transform.DOMove(target - offset, animationDuration);
            _rotateTween = transform.DODynamicLookAt(target + offset, animationDuration);
        }
    }
}