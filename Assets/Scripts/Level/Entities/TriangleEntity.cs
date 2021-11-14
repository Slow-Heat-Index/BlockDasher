using Level.Generator;
using Level.Player.Behaviour;
using Sources.Identification;
using Sources.Level;
using UnityEngine;

namespace Level.Entities {
    public class TriangleEntity : AggressiveEntity {
        protected override void OnPlayerCollision(DashData dashData) {
            dashData.Player.Lose(false);
        }

        public class TriangleEntityType : EntityType {
            public static readonly TriangleEntityType Instance = new TriangleEntityType();

            private TriangleEntityType() : base(Identifiers.Triangle, "Triangle") {
            }

            public override Entity SpawnEntity(BlockPosition position) {
                var o = Instantiate(FindObjectOfType<LevelGenerator>().triangle);
                var entity = o.GetComponent<Entity>();
                entity.InitPosition(position.Position, position.World);
                position.World.AddEntity(entity);
                return entity;
            }

            public override GameObject GetSpawnerPrefab() {
                return FindObjectOfType<EditorData>().triangleDisplay;
            }
        }
    }
}