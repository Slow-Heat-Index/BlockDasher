using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class CoconutBlock : Block {
        public CoconutBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Coconut, CoconutBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<CoconutBlockView>();

        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class CoconutBlockType : BlockType {
            public static readonly CoconutBlockType Instance = new CoconutBlockType();

            private CoconutBlockType() : base(
                Identifiers.Coconut,
                "Coconut",
                new Aabb(0.2f, 0, 0.2f, 0.6f, 1, 0.6f),
                1,
                true,
                Resources.Load<Mesh>("Models/Blocks/Coconut/Model"),
                Resources.Load<Texture>("Models/Blocks/Coconut/Default")
            ) {
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new CoconutBlock(position, data);
            }
        }
    }
}