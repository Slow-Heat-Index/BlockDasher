using Level.Generator;
using Level.Player.Behaviour;
using Sources.Identification;
using Sources.Level;
using Sources.Util;
using UnityEngine;

namespace Level.Entities {
    public class SphereEntity : AggressiveEntity {
        protected override void OnPlayerCollision(DashData dashData) {
            dashData.Cancel();
            Dashing = false;
            dashData.Player.transform.LookAt(dashData.Player.transform.position + Direction.GetVector());
            dashData.MovementBehaviour.ExecuteDash(dashData.With(Direction), 1);
        }

        public class SphereEntityType : EntityType {
            public static readonly SphereEntityType Instance = new SphereEntityType();

            private SphereEntityType() : base(Identifiers.Sphere, "Sphere") {
            }

            public override Entity SpawnEntity(BlockPosition position) {
                var o = Instantiate(FindObjectOfType<LevelGenerator>().sphere);
                var entity = o.GetComponent<Entity>();
                entity.InitPosition(position.Position, position.World);
                position.World.AddEntity(entity);
                return entity;
            }

            public override GameObject GetSpawnerPrefab() {
                return FindObjectOfType<EditorData>().sphereDisplay;
            }
        }
    }
}