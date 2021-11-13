using System;
using Level.Player.Data;
using Sources.Level.Raycast;
using Sources.Util;

namespace Level.Entities {
    public class AggressiveEntity : RandomPathAwareEntity {
        private static readonly Direction[] CheckDirections =
            { Direction.North, Direction.East, Direction.South, Direction.West };

        private PlayerData _playerData;

        private void Start() {
            _playerData = FindObjectOfType<PlayerData>();
        }

        public override void AfterFall() {
            // Don't nothing, just KILL.
            if(_playerData.BlockPosition.Position == Position.Position) return;
            
            if (!IsPlayerVisible()) {
                base.AfterFall();
                return;
            }

            var minDirection = Direction.Up;
            var minDistance = int.MaxValue;

            var playerPos = _playerData.BlockPosition.Position;
            foreach (var direction in CheckDirections) {
                if (!CanDashTo(direction, out var position)) continue;

                var distance = Math.Abs(position.x - playerPos.x) + Math.Abs(position.z - playerPos.z);
                if (distance >= minDistance) continue;
                minDistance = distance;
                minDirection = direction;
            }

            if (minDirection != Direction.Up) {
                ExecuteDash(minDirection);
            }
        }


        private bool IsPlayerVisible() {
            if (Position.Position.y != _playerData.BlockPosition.Position.y) return false;
            if ((Position.Position - _playerData.BlockPosition.Position).sqrMagnitude > 100) return false;

            var dir =  _playerData.BlockPosition.Position - Position.Position;
            var caster = new BlockRaycaster(Position.World, Position.Position,
                dir, 10);
            caster.Run();
            return caster.Result == null
                   || (caster.Result.Position.Position - Position.Position).sqrMagnitude >= dir.sqrMagnitude;
        }
    }
}