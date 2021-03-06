using Level.Generator;
using Level.Player;
using Level.Player.Behaviour;
using Sources.Identification;
using Sources.Level;
using UnityEngine;

namespace Level.Entities {
    public class SharkEntity : AggressiveEntity {
        public SharkEntity() {
            ExtraSteps = 1;
            avoidBlocks.Add(null);
        }

        protected override void OnPlayerCollision(DashData dashData) {
            _enemySoundManager.Play(2);
            dashData.Player.Lose(PlayerDeathCause.PUNCH);
        }

        public class SharkEntityType : EntityType {
            public static readonly SharkEntityType Instance = new SharkEntityType();

            private SharkEntityType() : base(Identifiers.Shark, "Shark") {
            }

            public override Entity SpawnEntity(BlockPosition position) {
                var level = FindObjectOfType<LevelGenerator>();
                var o = Instantiate(level.shark, level.transform);
                var entity = o.GetComponent<Entity>();
                entity.InitPosition(position.Position, position.World);
                position.World.AddEntity(entity);
                return entity;
            }

            public override GameObject GetSpawnerPrefab() {
                return FindObjectOfType<EditorData>().sharkDisplay;
            }
        }
    }
}