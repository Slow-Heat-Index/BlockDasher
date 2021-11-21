using Level.Generator;
using Level.Player.Behaviour;
using Level.Player.Data;
using Sources.Identification;
using Sources.Level;
using Sources.Util;
using UnityEngine;

namespace Level.Entities {
    public class SphereEntity : AggressiveEntity {
        
        private static readonly int AnimatorMove = Animator.StringToHash("Move");

        private Animator _animator;

        protected override void Start() {
            base.Start();
            _animator = GetComponentInChildren<Animator>();
        }

        public override void BeforeDash(PlayerData player) {
            base.BeforeDash(player);
            if (Dashing) {
                _animator.Play(AnimatorMove, 0);
            }
        }

        protected override void OnPlayerCollision(DashData dashData) {
            dashData.Cancel();
            Dashing = false;
            _enemySoundManager.Play(2);
            dashData.Player.transform.LookAt(dashData.Player.transform.position + Direction.GetVector());
            dashData.MovementBehaviour.ExecuteDash(dashData.With(Direction), 2);
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