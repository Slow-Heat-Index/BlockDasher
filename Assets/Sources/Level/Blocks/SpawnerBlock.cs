using Level;
using Level.Blocks;
using Level.Entities;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Registration;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class SpawnerBlock : Block {
        public SpawnerBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Spawner, SpawnerBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<SpawnerBlockView>();
        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => true;
        public override bool IsClimbableFrom(Direction direction) => false;

        public void Spawn() {
            var manager = Registry.Get<EntityType>(Identifiers.ManagerEntity);
            var id = GetMetadata(MetadataSnapshots.MetadataEntityType.Key);
            var type = manager.Get(id == null ? Identifiers.Triangle : new Identifier(id));
            type?.SpawnEntity(Position);
            if (GetMetadataBoolean(MetadataSnapshots.MetadataWaterlogged.Key)) {
                Position.World.PlaceBlock(new BlockData(Identifiers.Water), Position.Position);
            }
            else {
                Position.World.PlaceBlock(new BlockData(null), Position.Position);
            }
        }

        public class SpawnerBlockType : BlockType {
            public static readonly SpawnerBlockType Instance = new SpawnerBlockType();

            private SpawnerBlockType() : base(
                Identifiers.Spawner,
                "Spawner",
                new Aabb(0, 0, 0, 1, 1, 1),
                1,
                true,
                Resources.Load<Mesh>("Models/BlockModel"),
                Resources.Load<Texture>("Models/Blocks/Spawner/Item")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataEntityType.Key] = MetadataSnapshots.MetadataEntityType;
                DefaultMetadata[MetadataSnapshots.MetadataWaterlogged.Key] = MetadataSnapshots.MetadataWaterlogged;
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new SpawnerBlock(position, data);
            }
        }
    }
}