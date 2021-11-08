using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class FenceBlock : Block {
        public FenceBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Fence, FenceBlockType.Instance, position, data) {
        }


        public override Aabb CollisionBox {
            get {
                var dir = (Direction)GetMetadataEnum<Direction>(MetadataSnapshots.MetadataFacing.Key,
                    (int)Direction.North);
                return dir switch {
                    Direction.South => new Aabb(0, 0, 0, 1, 1, 4 / 20.0f),
                    Direction.East => new Aabb(16 / 20.0f, 0, 0, 4 / 20.0f, 1, 1),
                    Direction.West => new Aabb(0, 0, 0, 4 / 20.0f, 1, 1),
                    _ => new Aabb(0, 0, 16 / 20.0f, 1, 1, 4 / 20.0f)
                };
            }
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<FenceBlockView>();

        public override bool CanMoveTo(Direction direction) {
            return direction != (Direction)GetMetadataEnum<Direction>(MetadataSnapshots.MetadataFacing.Key,
                (int)Direction.North);
        }

        public override bool CanMoveFrom(Direction direction) {
            return direction != (Direction)GetMetadataEnum<Direction>(MetadataSnapshots.MetadataFacing.Key,
                (int)Direction.North);
        }

        public override bool IsClimbableFrom(Direction direction) => false;

        public class FenceBlockType : BlockType {
            public static readonly FenceBlockType Instance = new FenceBlockType();

            private FenceBlockType() : base(
                Identifiers.Fence,
                "Fence",
                new Aabb(0, 0, 0, 1, 1, 1),
                1,
                true,
                Resources.Load<Mesh>("Models/Blocks/Fence/Model"),
                Resources.Load<Texture>("Models/Blocks/Fence/Default")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataFacing.Key] = MetadataSnapshots.MetadataFacing;
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new FenceBlock(position, data);
            }
        }
    }
}