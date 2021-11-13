using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class BeachSandBlock : Block {
        public BeachSandBlock(BlockPosition position, BlockData data)
            : base(Identifiers.BeachSand, BeachSandBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<BeachSandBlockView>();
        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => true;

        public class BeachSandBlockType : BlockType {
            public static readonly BeachSandBlockType Instance = new BeachSandBlockType();

            private BeachSandBlockType() : base(
                Identifiers.BeachSand,
                "Beach Sand",
                new Aabb(0, 0, 0, 1, 1, 1),
                2,
                false,
                Resources.Load<Mesh>("Models/BlockModel"),
                Resources.Load<Texture>("Models/Blocks/BeachSand/White")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataForceTop.Key] = MetadataSnapshots.MetadataForceTop;
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new BeachSandBlock(position, data);
            }
        }
    }
}