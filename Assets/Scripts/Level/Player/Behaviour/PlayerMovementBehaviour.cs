using Level.Cameras.Behaviour;
using Level.Player.Data;
using Sources.Util;
using UnityEngine;

namespace Level.Player.Behaviour {
    [RequireComponent(typeof(PlayerData))]
    public class PlayerMovementBehaviour : MonoBehaviour {
        private PlayerData _data;
        private LevelCameraBehaviour _levelCameraBehaviour;

        private void Awake() {
            _data = GetComponent<PlayerData>();
            _levelCameraBehaviour = FindObjectOfType<LevelCameraBehaviour>();
        }

        public void Dash(Direction direction) {
            (direction != Direction.Up && direction != Direction.Down)
                .ValidateTrue($"Direction cannot be up or down! {direction}");

            if (!_data.CanPlayerMove || _data.hasWon) return;

            direction = direction.Rotated(_levelCameraBehaviour.direction);

            transform.LookAt(transform.position + direction.GetVector());

            if (!TryToClimb(direction)) {
                if (ExecuteDash(direction) == 0) return;
            }

            MoveRecursively(Direction.Down, 20);

            _data.BlockPosition.Block?.OnPlayerStopsIn(_data);
            if (_data.hasWon) return;

            var current = _data.BlockPosition.Block;
            if (current == null || current.CanMoveTo(Direction.Down)) {
                var down = _data.BlockPosition.Moved(Direction.Down).Block;
                if (down == null || down.CanMoveFrom(Direction.Up)) {
                    // OWO PLAYER IS DEAD
                    _data.Lose();
                }
            }

            if (_data.movementsLeft > 0) _data.movementsLeft--;
            if (_data.movementsLeft == 0) {
                //Death!
                _data.Lose();
            }

            _data.movements++;

            _levelCameraBehaviour.UpdateCameraPosition();
        }

        private bool TryToClimb(Direction direction) {
            var current = _data.BlockPosition.Block;
            if (current != null && (!current.CanMoveTo(direction) || !current.CanMoveTo(Direction.Up))) return false;

            var opposite = direction.GetOpposite();

            var next = _data.BlockPosition.Moved(direction).Block;
            if (next == null || next.CanMoveFrom(opposite) || !next.IsClimbableFrom(opposite)) return false;

            var up = _data.BlockPosition.Moved(Direction.Up).Block;
            if (up != null && (!up.CanMoveFrom(Direction.Down) || !up.CanMoveTo(direction))) return false;

            var nextUp = _data.BlockPosition.Moved(Direction.Up).Moved(direction).Block;
            if (nextUp != null && !nextUp.CanMoveFrom(opposite)) return false;

            _data.Move(Vector3Int.up);
            up?.OnPlayerStepsIn(_data);
            _data.Move(direction.GetVector());
            nextUp?.OnPlayerStepsIn(_data);
            return true;
        }

        private int ExecuteDash(Direction direction) {
            var blocksDashed = 0;
            var maximumMovements = _data.BlockPosition.Moved(Direction.Down).Block?.MaximumSteps ?? 2;
            while (blocksDashed < maximumMovements + _data.extraSteps) {
                var fromBlock = _data.BlockPosition.Block;
                var toBlock = _data.BlockPosition.Moved(direction).Block;

                if (fromBlock != null && !fromBlock.CanMoveTo(direction)
                    || toBlock != null && !toBlock.CanMoveFrom(direction.GetOpposite())) {
                    break;
                }

                _data.Move(direction.GetVector());
                toBlock?.OnPlayerStepsIn(_data);
                maximumMovements = _data.BlockPosition.Moved(Direction.Down).Block?.MaximumSteps ??
                                   maximumMovements;
                blocksDashed++;
            }

            return blocksDashed;
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
                toBlock?.OnPlayerStepsIn(_data);
                blocksLeft--;
            }
        }
    }
}