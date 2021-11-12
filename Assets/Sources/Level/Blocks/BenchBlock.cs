using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class BenchBlock : Block {
        public BenchBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Bench, BenchBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<BenchBlockView>();

        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class BenchBlockType : BlockType {
            public static readonly BenchBlockType Instance = new BenchBlockType();

            private BenchBlockType() : base(
                Identifiers.Bench,
                "Bench",
                new Aabb(0, 0, 0, 1, 0.5f, 1),
                1,
                true,
                Resources.Load<Mesh>("Models/Blocks/Bench/Model1"),
                Resources.Load<Texture>("Models/Blocks/Bench/Default")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataFacing.Key] = MetadataSnapshots.MetadataFacing;
                DefaultMetadata[MetadataSnapshots.MetadataInverse.Key] = MetadataSnapshots.MetadataInverse;
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new BenchBlock(position, data);
            }
        }
    }
}