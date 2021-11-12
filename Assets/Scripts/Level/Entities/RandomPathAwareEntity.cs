using System;
using System.Linq;
using Sources.Level;
using Sources.Util;

namespace Level.Entities {
    public class RandomPathAwareEntity : Entity {
        private readonly Random _random = new Random();

        public override void BeforeDash() {
        }

        public override void AfterDash() {
        }

        public override void AfterFall() {
            var directions = new[] { 2, 3, 4, 5 }.OrderBy(x => _random.Next());

            foreach (var dirIndex in directions) {
                var direction = (Direction)dirIndex;
                if (!CanDashTo(direction)) continue;
                ExecuteDash(direction);
                break;
            }
        }

        private bool CanDashTo(Direction direction) {
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

            down = position.Moved(Direction.Down).Block;
            return blocksDashed != 0 &&
                   (position.Block != null && !position.Block.CanMoveTo(Direction.Down) ||
                    down != null && !down.CanMoveFrom(Direction.Up));
        }

        private void ExecuteDash(Direction direction) {
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