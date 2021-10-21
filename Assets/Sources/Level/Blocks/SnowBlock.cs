using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class SnowBlock : Block {
        public SnowBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Snow, SnowBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<SnowBlockView>();
        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => true;

        public class SnowBlockType : BlockType {
            public static readonly SnowBlockType Instance = new SnowBlockType();

            private SnowBlockType() : base(
                Identifiers.Snow,
                "Snow",
                new Aabb(0, 0, 0, 1, 1, 1),
                3,
                Resources.Load<Mesh>("Models/BlockModel"),
                Resources.Load<Texture>("Models/Blocks/Snow/White")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataForceTop.Key] = MetadataSnapshots.MetadataForceTop;
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new SnowBlock(position, data);
            }
        }
    }
}