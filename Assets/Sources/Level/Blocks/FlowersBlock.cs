using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class FlowersBlock : Block {
        public FlowersBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Flowers, FlowersBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<FlowersBlockView>();

        public override bool CanMoveTo(Direction direction) {
            return true;
        }

        public override bool CanMoveFrom(Direction direction) {
            return true;
        }
        
        public class FlowersBlockType : BlockType {
            public static readonly FlowersBlockType Instance = new FlowersBlockType();

            private FlowersBlockType() : base(
                Identifiers.Flowers,
                "Flowers",
                new Aabb(0.2f, 0, 0.2f, 0.6f, 0.5f, 0.6f),
                Resources.Load<Mesh>("Models/Props/Flowers1/Model"),
                Resources.Load<Texture>("Models/Props/Flowers1/Default")
            ) {
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new FlowersBlock(position, data);
            }
        }
    }
}