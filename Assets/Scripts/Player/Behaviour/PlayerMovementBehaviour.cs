using Cameras.Behaviour;
using Player.Data;
using Sources.Util;
using UnityEngine;

namespace Player.Behaviour {
    [RequireComponent(typeof(PlayerData))]
    public class PlayerMovementBehaviour : MonoBehaviour {
        private PlayerData _data;
        private CameraBehaviour _cameraBehaviour;

        private void Awake() {
            _data = GetComponent<PlayerData>();
            _cameraBehaviour = FindObjectOfType<CameraBehaviour>();
        }

        public void Dash(Direction direction) {
            (direction != Direction.Up && direction != Direction.Down)
                .ValidateTrue($"Direction cannot be up or down! {direction}");
            
            direction = direction.Rotated(_cameraBehaviour.direction);

            MoveRecursively(direction, _data.blocksPerDash);
            //MoveRecursively(Direction.Down, 20);

            var current = _data.BlockPosition.Block;
            if (current == null || current.CanMoveTo(Direction.Down)) {
                var down = _data.BlockPosition.Moved(Direction.Down).Block;
                if (down == null || down.CanMoveFrom(Direction.Up)) {
                    // OWO PLAYER IS DEAD
                    _data.Teleport(_data.level.World.StartPosition.Position.Position);
                }
            }


            _cameraBehaviour.UpdateCameraPosition();
        }

        private void MoveRecursively(Direction direction, int blocks) {
            var blocksLeft = blocks;
            while (blocksLeft > 0) {
                var fromBlock = _data.BlockPosition.Block;
                var toBlock = _data.BlockPosition.Moved(direction).Block;

                if (fromBlock != null && !fromBlock.CanMoveTo(direction)
                    || toBlock != null && !toBlock.CanMoveFrom(direction.GetOpposite())) {
                    blocksLeft = 0;
                    continue;
                }

                _data.Move(direction.GetVector());
                blocksLeft--;
            }
        }
    }
}