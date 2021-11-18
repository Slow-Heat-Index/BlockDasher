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

            if (!_data.CanPlayerMove || _data.hasWon || _data.dead) return;

            _data.BlockPosition.World.ForEachEntity(e => e.BeforeDash(_data));

            direction = direction.Rotated(_levelCameraBehaviour.direction);

            transform.LookAt(transform.position + direction.GetVector());

            var dashData = new DashData(_data, this, direction);

            if (!TryToClimb(dashData)) {
                if (ExecuteDash(dashData, _data.extraSteps) == 0) return;
            }

            _data.BlockPosition.World.ForEachEntity(e => e.AfterDash(dashData));

            if (!_data.dead) {
                MoveRecursively(Direction.Down, 20);
            }

            _data.BlockPosition.Block?.OnPlayerStopsIn(_data);
            _data.BlockPosition.World.ForEachEntity(e => e.AfterFall(dashData));
            if (_data.hasWon) return;
            _data.FinishMoving();
        }

        private bool TryToClimb(DashData dash) {
            var current = _data.BlockPosition.Block;
            if (current != null && (!current.CanMoveTo(dash.Direction) || !current.CanMoveTo(Direction.Up)))
                return false;

            var opposite = dash.Direction.GetOpposite();

            var next = _data.BlockPosition.Moved(dash.Direction).Block;
            if (next == null || next.CanMoveFrom(opposite) || !next.IsClimbableFrom(opposite)) return false;

            var up = _data.BlockPosition.Moved(Direction.Up).Block;
            if (up != null && (!up.CanMoveFrom(Direction.Down) || !up.CanMoveTo(dash.Direction))) return false;

            var nextUp = _data.BlockPosition.Moved(Direction.Up).Moved(dash.Direction).Block;
            if (nextUp != null && !nextUp.CanMoveFrom(opposite)) return false;

            _data.nextMoveJump = true;
            _data.Move(Vector3Int.up);
            up?.OnPlayerStepsIn(_data);
            _data.BlockPosition.World.ForEachEntity(e => e.AfterMove(dash));

            if (_data.dead || dash.Cancelled) {
                dash.Cancel();
                return true;
            }

            _data.Move(dash.Direction.GetVector());
            nextUp?.OnPlayerStepsIn(_data);
            _data.BlockPosition.World.ForEachEntity(e => e.AfterMove(dash));
            dash.Cancel();
            return true;
        }

        public int ExecuteDash(DashData dash, int extraSteps) {
            var blocksDashed = 0;
            var maximumMovements = _data.BlockPosition.Moved(Direction.Down).Block?.MaximumSteps ?? 2;
            while (!dash.Cancelled) {
                var fromBlock = _data.BlockPosition.Block;
                var toBlock = _data.BlockPosition.Moved(dash.Direction).Block;

                if (fromBlock != null && !fromBlock.CanMoveTo(dash.Direction)
                    || toBlock != null && !toBlock.CanMoveFrom(dash.Direction.GetOpposite())) {
                    break;
                }

                _data.Move(dash.Direction.GetVector());
                _data.BlockPosition.World.ForEachEntity(e => e.AfterMove(dash));
                toBlock?.OnPlayerStepsIn(_data);

                var down = _data.BlockPosition.Moved(Direction.Down).Block;
                if (down is { BehavesLikeAir: false }) {
                    maximumMovements = down.MaximumSteps;
                }

                blocksDashed++;

                if (blocksDashed >= maximumMovements + extraSteps || _data.dead) {
                    dash.Cancel();
                }
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