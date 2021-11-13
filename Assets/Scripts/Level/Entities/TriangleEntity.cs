using Level.Generator;
using Sources.Identification;
using Sources.Level;

namespace Level.Entities {
    public class TriangleEntity : AggressiveEntity {
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
        }
    }
}