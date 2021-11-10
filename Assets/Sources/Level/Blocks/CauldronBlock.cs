using Level;
using Level.Blocks;
using Level.Player.Data;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class CauldronBlock : Block {
        public CauldronBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Cauldron, CauldronBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<CauldronBlockView>();
        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => false;
        
        public class CauldronBlockType : BlockType {
            public static readonly CauldronBlockType Instance = new CauldronBlockType();

            private CauldronBlockType() : base(
                Identifiers.Cauldron,
                "Cauldron",
                new Aabb(0.2f, 0, 0.2f, 0.6f, 1, 0.6f),
                1,
                true,
                Resources.Load<Mesh>("Models/Blocks/Cauldron/Model"),
                Resources.Load<Texture>("Models/Blocks/Cauldron/Default")
            ) {
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new CauldronBlock(position, data);
            }
        }
    }
}