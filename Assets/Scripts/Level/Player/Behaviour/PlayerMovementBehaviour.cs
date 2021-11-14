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
                .ValidateTrue("Direction cannot be up or down!");

            if (!_data.CanPlayerMove || _data.hasWon) return;

            _data.BlockPosition.World.ForEachEntity(e => e.BeforeDash(_data));

            direction = direction.Rotated(_levelCameraBehaviour.direction);

            transform.LookAt(transform.position + direction.GetVector());

            if (!TryToClimb(direction)) {
                if (ExecuteDash(direction) == 0) return;
            }

            _data.BlockPosition.World.ForEachEntity(e => e.AfterDash(_data));

            if (!_data.dead) {
                MoveRecursively(Direction.Down, 20);
            }

            _data.BlockPosition.Block?.OnPlayerStopsIn(_data);
            _data.BlockPosition.World.ForEachEntity(e => e.AfterFall(_data));
            if (_data.hasWon) return;
            _data.FinishMoving();
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
            _data.BlockPosition.World.ForEachEntity(e => e.AfterMove(_data, _data.BlockPosition.Position));

            if (_data.dead) return true;

            _data.Move(direction.GetVector());
            nextUp?.OnPlayerStepsIn(_data);
            _data.BlockPosition.World.ForEachEntity(e => e.AfterMove(_data, _data.BlockPosition.Position));
            return true;
        }

        private int ExecuteDash(Direction direction) {
            var blocksDashed = 0;
            var maximumMovements = _data.BlockPosition.Moved(Direction.Down).Block?.MaximumSteps ?? 2;
            while (blocksDashed < maximumMovements + _data.extraSteps && !_data.dead) {
                var fromBlock = _data.BlockPosition.Block;
                var toBlock = _data.BlockPosition.Moved(direction).Block;

                if (fromBlock != null && !fromBlock.CanMoveTo(direction)
                    || toBlock != null && !toBlock.CanMoveFrom(direction.GetOpposite())) {
                    break;
                }

                _data.Move(direction.GetVector());
                _data.BlockPosition.World.ForEachEntity(e => e.AfterMove(_data, _data.BlockPosition.Position));
                toBlock?.OnPlayerStepsIn(_data);

                var down = _data.BlockPosition.Moved(Direction.Down).Block;
                if (down is { BehavesLikeAir: false }) {
                    maximumMovements = down.MaximumSteps;
                }

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