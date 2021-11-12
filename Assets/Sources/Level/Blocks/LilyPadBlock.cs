using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class LilyPadBlock : Block {
        public LilyPadBlock(BlockPosition position, BlockData data)
            : base(Identifiers.LilyPad, LilyPadBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<LilyPadBlockView>();
        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => true;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class LilyPadBlockType : BlockType {
            public static readonly LilyPadBlockType Instance = new LilyPadBlockType();

            private LilyPadBlockType() : base(
                Identifiers.LilyPad,
                "Lily pad",
                new Aabb(0.2f, 0, 0.2f, 0.6f, 0.1f, 0.6f),
                2,
                true,
                Resources.Load<Mesh>("Models/Blocks/LilyPad/Model"),
                Resources.Load<Texture>("Models/Blocks/LilyPad/Default")
            ) {
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new LilyPadBlock(position, data);
            }
        }
    }
}