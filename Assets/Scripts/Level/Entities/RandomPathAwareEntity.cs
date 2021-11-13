using System.Linq;
using Sources.Level;
using Sources.Util;
using UnityEngine;
using Random = System.Random;

namespace Level.Entities {
    public class RandomPathAwareEntity : Entity {
        protected readonly Random Random = new Random();

        public override void BeforeDash() {
        }

        public override void AfterDash() {
        }

        public override void AfterFall() {
            var directions = new[] { 2, 3, 4, 5 }.OrderBy(x => Random.Next());

            foreach (var dirIndex in directions) {
                var direction = (Direction)dirIndex;
                if (!CanDashTo(direction, out var _)) continue;
                ExecuteDash(direction);
                break;
            }
        }

        protected bool CanDashTo(Direction direction, out Vector3Int finalPosition) {
            var blocksDashed = 0;
            var maximumMovements = Position.Moved(Direction.Down).Block?.MaximumSteps ?? 2;
            var position = Position;
            Block down;
            while (blocksDashed < maximumMovements) {
                var fromBlock = position.Block;
                var toBlock = position.Moved(direction).Block;

                if (fromBlock != null && !fromBlock.CanMoveTo(direction)
                    || toBlock != null && !toBlock.CanMoveFrom(direction.GetOpposite())) {
                    break;
                }

                position = position.Moved(direction.GetVector());
                down = position.Moved(Direction.Down).Block;
                if (down is { BehavesLikeAir: false }) {
                    maximumMovements = down.MaximumSteps;
                }

                blocksDashed++;
            }

            finalPosition = position.Position;
            down = position.Moved(Direction.Down).Block;
            return blocksDashed != 0 &&
                   (position.Block != null && !position.Block.CanMoveTo(Direction.Down) ||
                    down != null && !down.CanMoveFrom(Direction.Up));
        }

        protected void ExecuteDash(Direction direction) {
            transform.LookAt(transform.position + direction.GetVector());
            var blocksDashed = 0;
            var maximumMovements = Position.Moved(Direction.Down).Block?.MaximumSteps ?? 2;
            while (blocksDashed < maximumMovements) {
                var fromBlock = Position.Block;
                var toBlock = Position.Moved(direction).Block;

                if (fromBlock != null && !fromBlock.CanMoveTo(direction)
                    || toBlock != null && !toBlock.CanMoveFrom(direction.GetOpposite())) {
                    break;
                }

                Move(direction.GetVector());

                var down = Position.Moved(Direction.Down).Block;
                if (down is { BehavesLikeAir: false }) {
                    maximumMovements = down.MaximumSteps;
                }

                blocksDashed++;
            }
        }
    }
}