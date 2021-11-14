using System.Linq;
using Level.Player.Behaviour;
using Level.Player.Data;
using Sources.Level;
using Sources.Util;
using UnityEngine;
using Random = System.Random;

namespace Level.Entities {
    public class RandomPathAwareEntity : Entity {
        protected readonly Random Random = new Random();


        protected Direction Direction = Direction.North;
        protected bool DirectionFound;
        protected int BlocksDashed;
        protected int MaximumMovements;
        protected bool Dashing;

        protected bool CollidedWithPlayer = false;

        public override void BeforeDash(PlayerData player) {
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
                Dashing = true;
                transform.LookAt(transform.position + direction.GetVector());
                break;
            }
        }

        public override void AfterMove(DashData dashData) {
            DashStep(dashData);
        }

        public override void AfterDash(DashData dashData) {
            while (Dashing) {
                DashStep(dashData);
            }
        }

        protected virtual void OnPlayerCollision(DashData dashData) {
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

            if (!CollidedWithPlayer && dash.Player.BlockPosition.Position == Position.Position) {
                CollidedWithPlayer = true;
                OnPlayerCollision(dash);
            }

            if (!Dashing) return;
            Move(Direction.GetVector());

            if (!CollidedWithPlayer && dash.Player.BlockPosition.Position == Position.Position) {
                CollidedWithPlayer = true;
                OnPlayerCollision(dash);
            }

            var down = Position.Moved(Direction.Down).Block;
            if (down is { BehavesLikeAir: false }) {
                MaximumMovements = down.MaximumSteps;
            }

            BlocksDashed++;
        }
    }
}