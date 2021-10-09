using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class EndBlock : Block {
        public EndBlock(BlockPosition position, BlockData data)
            : base(Identifiers.End, EndBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<EndBlockView>();

        public override bool CanMoveTo(Direction direction) {
            return true;
        }

        public override bool CanMoveFrom(Direction direction) {
            return true;
        }
        
        public class EndBlockType : BlockType {
            public static readonly EndBlockType Instance = new EndBlockType();

            private EndBlockType() : base(
                Identifiers.End,
                "End",
                new Aabb(0, 0, 0, 1, 0.1f, 1),
                Resources.Load<Mesh>("Models/StartEndModel"),
                Resources.Load<Texture>("Models/End/Default")
            ) {
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new EndBlock(position, data);
            }
        }
    }
}