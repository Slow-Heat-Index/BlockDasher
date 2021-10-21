using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class FenceCornerBlock : Block {
        private static readonly Direction[] Rotated =
            { Direction.East, Direction.West, Direction.South, Direction.North };

        public FenceCornerBlock(BlockPosition position, BlockData data)
            : base(Identifiers.FenceCorner, FenceCornerBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<FenceCornerBlockView>();

        public override bool CanMoveTo(Direction direction) {
            var dir = (Direction)GetMetadataEnum<Direction>(MetadataSnapshots.MetadataFacing.Key,
                (int)Direction.North);
            return direction != dir && direction != Rotated[(int)dir - 2];
        }

        public override bool CanMoveFrom(Direction direction) {
            var dir = (Direction)GetMetadataEnum<Direction>(MetadataSnapshots.MetadataFacing.Key,
                (int)Direction.North);
            return direction != dir && direction != Rotated[(int)dir - 2];
        }

        public override bool IsClimbableFrom(Direction direction) => false;

        public class FenceCornerBlockType : BlockType {
            public static readonly FenceCornerBlockType Instance = new FenceCornerBlockType();

            private FenceCornerBlockType() : base(
                Identifiers.FenceCorner,
                "Fence Corner",
                new Aabb(0, 0, 0, 1, 1, 1),
                1,
                Resources.Load<Mesh>("Models/Blocks/FenceCorner/Model"),
                Resources.Load<Texture>("Models/Blocks/FenceCorner/Default")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataFacing.Key] = MetadataSnapshots.MetadataFacing;
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new FenceCornerBlock(position, data);
            }
        }
    }
}