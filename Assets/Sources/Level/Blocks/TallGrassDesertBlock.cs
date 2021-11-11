using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class TallGrassDesertBlock : Block {
        public TallGrassDesertBlock(BlockPosition position, BlockData data)
            : base(Identifiers.TallGrassDesert, TallGrassDesertBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<TallGrassDesertBlockView>();
        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => true;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class TallGrassDesertBlockType : BlockType {
            public static readonly TallGrassDesertBlockType Instance = new TallGrassDesertBlockType();

            private TallGrassDesertBlockType() : base(
                Identifiers.TallGrassDesert,
                "Desert Tall Grass",
                new Aabb(0.1f, 0, 0.1f, 0.8f, 0.2f, 0.8f),
                2,
                true,
                Resources.Load<Mesh>("Models/Blocks/TallGrassDesert/Model"),
                Resources.Load<Texture>("Models/Blocks/TallGrassDesert/Default")
            ) {
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new TallGrassDesertBlock(position, data);
            }
        }
    }
}