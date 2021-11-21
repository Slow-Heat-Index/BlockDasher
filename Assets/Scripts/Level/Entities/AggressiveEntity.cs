using System;
using Level.Player.Data;
using Sources.Identification;
using Sources.Level.Raycast;
using Sources.Util;
using UnityEngine;

namespace Level.Entities {
    [RequireComponent(typeof(EnemySoundManager))]
    public class AggressiveEntity : RandomPathAwareEntity {
        private static readonly Direction[] CheckDirections =
            { Direction.North, Direction.East, Direction.South, Direction.West };

        private PlayerData _playerData;
        private bool previousSeen;
        private bool soundPlayed;

        protected EnemySoundManager _enemySoundManager;

        protected override void Start() {
            _playerData = FindObjectOfType<PlayerData>();
            _enemySoundManager = GetComponent<EnemySoundManager>();
            base.Start();
        }
        

        protected override void CalculateNextDash(bool dashing) {
            DirectionFound = false;
            BlocksDashed = 0;
            CollidedWithPlayer = false;

            if (_playerData.BlockPosition.Position == Position.Position || !IsPlayerVisible()) {
                previousSeen = false;
                return;
            }

            if (!previousSeen) {
                previousSeen = true;
                if (!soundPlayed) {
                    _enemySoundManager.Play(0);
                    soundPlayed = true;
                }
                
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
            
            if(minDirection == Direction.Up) return;

            DirectionFound = true;
            if (previousSeen == false) {
                _enemySoundManager.Play(0);
                previousSeen = true;
            }
            Direction = minDirection;
            MaximumMovements = Position.Moved(Direction.Down).Block?.MaximumSteps ?? 2;
            MaximumMovements += ExtraSteps;
            Dashing = true;

            if (!dashing) {
                transform.LookAt(transform.position + Direction.GetVector());
            }
        }


        private bool IsPlayerVisible() {
            if (Position.Position.y != _playerData.BlockPosition.Position.y) return false;
            if ((Position.Position - _playerData.BlockPosition.Position).sqrMagnitude > 100) return false;

            var dir = _playerData.BlockPosition.Position - Position.Position;
            var caster = new BlockRaycaster(Position.World, Position.Position,
                dir, 10);
            
            caster.BypassBlocks.Add(Identifiers.Water);
            caster.BypassBlocks.Add(Identifiers.End);
            caster.BypassBlocks.Add(Identifiers.Start);
            caster.BypassBlocks.Add(Identifiers.Spawner);
            caster.BypassBlocks.Add(Identifiers.Grass);
            caster.BypassBlocks.Add(Identifiers.TallGrassDesert);
            caster.BypassBlocks.Add(Identifiers.Liana);
            caster.BypassBlocks.Add(Identifiers.Flowers);
            caster.BypassBlocks.Add(Identifiers.BeachFlowers);

            caster.Run();
            return caster.Result == null
                   || (caster.Result.Position.Position - Position.Position).sqrMagnitude >= dir.sqrMagnitude;
        }
    }
}