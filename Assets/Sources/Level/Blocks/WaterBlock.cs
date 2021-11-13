using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class WaterBlock : Block {
        public WaterBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Water, WaterBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<WaterBlockView>();
        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => true;
        public override bool IsClimbableFrom(Direction direction) => false;
        
        public class WaterBlockType : BlockType {
            public static readonly WaterBlockType Instance = new WaterBlockType();

            private WaterBlockType() : base(
                Identifiers.Water,
                "Water",
                new Aabb(0, 0, 0, 1, 1, 1),
                1,
                true,
                Resources.Load<Mesh>("Models/BlockModel"),
                Resources.Load<Texture>("Models/Blocks/Water/Default")
            ) {
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new WaterBlock(position, data);
            }
        }
    }
}