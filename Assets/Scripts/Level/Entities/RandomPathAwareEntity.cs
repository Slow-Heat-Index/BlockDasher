using System.Collections.ObjectModel;
using System.Linq;
using Level.Player.Behaviour;
using Sources.Identification;
using Sources.Level;
using Sources.Util;
using UnityEngine;
using Random = System.Random;

namespace Level.Entities {
    public class RandomPathAwareEntity : Entity {
        protected readonly Random Random = new Random();


        protected readonly Collection<Identifier> avoidBlocks = new Collection<Identifier>();
        protected int ExtraSteps;
        protected Direction Direction = Direction.North;
        protected bool DirectionFound;
        protected int BlocksDashed;
        protected int MaximumMovements;
        protected bool Dashing;

        protected bool CollidedWithPlayer = false;

        protected override void Start() {
            base.Start();
            CalculateNextDash(false);
            transform.LookAt(transform.position + Direction.GetVector());
        }

        public override void AfterMove(DashData dashData) {
            if (!CollidedWithPlayer && dashData.Player.BlockPosition.Position == Position.Position) {
                CollidedWithPlayer = true;
                OnPlayerCollision(dashData);
            }

            DashStep(dashData);
        }

        public override void AfterDash(DashData dashData) {
            if (!CollidedWithPlayer && dashData.Player.BlockPosition.Position == Position.Position) {
                CollidedWithPlayer = true;
                OnPlayerCollision(dashData);
            }

            while (Dashing) {
                DashStep(dashData);
            }

            if (CollidedWithPlayer || dashData.Player.BlockPosition.Position != Position.Position) return;
            CollidedWithPlayer = true;
            OnPlayerCollision(dashData);
        }

        public override void AfterFall(DashData dashData) {
            base.AfterFall(dashData);

            if (!CollidedWithPlayer && dashData.Player.BlockPosition.Position == Position.Position) {
                CollidedWithPlayer = true;
                OnPlayerCollision(dashData);
            }

            CalculateNextDash(BlocksDashed > 0);
        }

        protected virtual void OnPlayerCollision(DashData dashData) {
        }

        public override void OnTweenComplete() {
            if (DirectionFound) {
                transform.LookAt(transform.position + Direction.GetVector());
            }
        }

        protected virtual void CalculateNextDash(bool dashing) {
            DirectionFound = false;
            BlocksDashed = 0;
            CollidedWithPlayer = false;

            var directions = new[] { 2, 3, 4, 5 }.OrderBy(x => Random.Next());
            foreach (var dirIndex in directions) {
                var direction = (Direction)dirIndex;
                if (!CanDashTo(direction, out _)) continue;
                DirectionFound = true;
                Direction = direction;
                MaximumMovements = Position.Moved(Direction.Down).Block?.MaximumSteps ?? 2;
                MaximumMovements += ExtraSteps;
                Dashing = true;

                if (!dashing) {
                    transform.LookAt(transform.position + Direction.GetVector());
                }
                
                break;
            }
        }

        protected bool CanDashTo(Direction direction, out Vector3Int finalPosition) {
            var blocksDashed = 0;
            var maximumMovements = Position.Moved(Direction.Down).Block?.MaximumSteps ?? 2;
            maximumMovements += ExtraSteps;
            var position = Position;
            Block down;
            while (blocksDashed < maximumMovements) {
                var fromBlock = position.Block;
                var toBlock = position.Moved(direction).Block;

                if (fromBlock != null && !fromBlock.CanMoveTo(direction)
                    || toBlock != null && !toBlock.CanMoveFrom(direction.GetOpposite())) {
                    break;
                }

                if (toBlock != null && avoidBlocks.Contains(toBlock.Identifier)
                    || toBlock == null && avoidBlocks.Contains(null)) {
                    finalPosition = position.Position;
                    return false;
                }

                position = position.Moved(direction);
                down = position.Moved(Direction.Down).Block;
                if (down is { BehavesLikeAir: false }) {
                    maximumMovements = down.MaximumSteps + ExtraSteps;
                }

                blocksDashed++;
            }

            finalPosition = position.Position;
            down = position.Moved(Direction.Down).Block;
            return blocksDashed != 0 &&
                   (position.Block != null && !position.Block.CanMoveTo(Direction.Down) ||
                    down != null && !down.CanMoveFrom(Direction.Up));
        }

        protected void DashStep(DashData dash) {
            if (!Dashing) return;
            if (!DirectionFound) {
                Dashing = false;
                return;
            }

            if (BlocksDashed >= MaximumMovements) {
                Dashing = false;
                return;
            }

            var fromBlock = Position.Block;
            var toBlock = Position.Moved(Direction).Block;

            if (fromBlock != null && !fromBlock.CanMoveTo(Direction)
                || toBlock != null && !toBlock.CanMoveFrom(Direction.GetOpposite())) {
                Dashing = false;
                return;
            }

            if (!Dashing) return;
            Move(Direction.GetVector());

            if (!CollidedWithPlayer && dash.Player.BlockPosition.Position == Position.Position) {
                CollidedWithPlayer = true;
                OnPlayerCollision(dash);
            }

            var down = Position.Moved(Direction.Down).Block;
            if (down is { BehavesLikeAir: false }) {
                MaximumMovements = down.MaximumSteps + ExtraSteps;
            }

            BlocksDashed++;
        }
    }
}