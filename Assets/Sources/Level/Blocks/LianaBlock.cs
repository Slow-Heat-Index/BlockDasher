using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class LianaBlock : Block {
        public LianaBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Liana, LianaBlockType.Instance, position, data) {
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

        public override BlockView GenerateBlockView() => GameObject.AddComponent<LianaBlockView>();

        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => true;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class LianaBlockType : BlockType {
            public static readonly LianaBlockType Instance = new LianaBlockType();

            private LianaBlockType() : base(
                Identifiers.Liana,
                "Liana",
                new Aabb(0, 0, 0, 1, 1, 1),
                1,
                true,
                Resources.Load<Mesh>("Models/Blocks/Liana/Model"),
                Resources.Load<Texture>("Models/Blocks/Liana/Default")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataFacing.Key] = MetadataSnapshots.MetadataFacing;
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new LianaBlock(position, data);
            }
        }
    }
}