using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class SandBlock : Block {
        public SandBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Sand, SandBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<SandBlockView>();
        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => true;
        
        public class SandBlockType : BlockType {
            public static readonly SandBlockType Instance = new SandBlockType();

            private SandBlockType() : base(
                Identifiers.Sand,
                "Sand",
                new Aabb(0, 0, 0, 1, 1, 1),
                2,
                false,
                Resources.Load<Mesh>("Models/BlockModel"),
                Resources.Load<Texture>("Models/Blocks/Sand/White")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataForceTop.Key] = MetadataSnapshots.MetadataForceTop;
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new SandBlock(position, data);
            }
        }
    }
}