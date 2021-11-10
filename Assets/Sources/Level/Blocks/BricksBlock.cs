using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class BricksBlock : Block {
        public BricksBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Bricks, BricksBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<BricksBlockView>();
        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => true;

        public class BricksBlockType : BlockType {
            public static readonly BricksBlockType Instance = new BricksBlockType();

            private BricksBlockType() : base(
                Identifiers.Bricks,
                "bricks",
                new Aabb(0, 0, 0, 1, 1, 1),
                2,
                false,
                Resources.Load<Mesh>("Models/BlockFacesModel"),
                Resources.Load<Texture>("Models/Blocks/Bricks/Top1")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataForceTop.Key] = MetadataSnapshots.MetadataForceTop;
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new BricksBlock(position, data);
            }
        }
    }
}