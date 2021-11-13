using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class QuicksandBlock : Block {
        public QuicksandBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Quicksand, QuicksandBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<QuicksandBlockView>();
        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => true;
        
        public class QuicksandBlockType : BlockType {
            public static readonly QuicksandBlockType Instance = new QuicksandBlockType();

            private QuicksandBlockType() : base(
                Identifiers.Quicksand,
                "Quicksand",
                new Aabb(0, 0, 0, 1, 1, 1),
                1,
                false,
                Resources.Load<Mesh>("Models/BlockModel"),
                Resources.Load<Texture>("Models/Blocks/Quicksand/White")
            ) {
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new QuicksandBlock(position, data);
            }
        }
    }
}