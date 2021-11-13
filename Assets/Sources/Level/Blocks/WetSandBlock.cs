using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class WetSandBlock : Block {
        public WetSandBlock(BlockPosition position, BlockData data)
            : base(Identifiers.WetSand, WetSandBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<WetSandBlockView>();
        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => true;
        
        public class WetSandBlockType : BlockType {
            public static readonly WetSandBlockType Instance = new WetSandBlockType();

            private WetSandBlockType() : base(
                Identifiers.WetSand,
                "WetSand",
                new Aabb(0, 0, 0, 1, 1, 1),
                1,
                false,
                Resources.Load<Mesh>("Models/BlockModel"),
                Resources.Load<Texture>("Models/Blocks/WetSand/White")
            ) {
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new WetSandBlock(position, data);
            }
        }
    }
}