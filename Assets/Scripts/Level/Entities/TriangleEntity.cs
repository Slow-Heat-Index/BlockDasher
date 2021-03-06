using Level.Generator;
using Level.Player;
using Level.Player.Behaviour;
using Level.Player.Data;
using Sources.Identification;
using Sources.Level;
using UnityEngine;

namespace Level.Entities {
    public class TriangleEntity : AggressiveEntity {
        
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
                
                _enemySoundManager.Play(1);
            }
        }

        protected override void OnPlayerCollision(DashData dashData) {
            dashData.Player.Lose(PlayerDeathCause.PUNCH);
            _enemySoundManager.Play(2);
        }

        public class TriangleEntityType : EntityType {
            public static readonly TriangleEntityType Instance = new TriangleEntityType();

            private TriangleEntityType() : base(Identifiers.Triangle, "Triangle") {
            }

            public override Entity SpawnEntity(BlockPosition position) {
                var level = FindObjectOfType<LevelGenerator>();
                var o = Instantiate(level.triangle, level.transform);
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