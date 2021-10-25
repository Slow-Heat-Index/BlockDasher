using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class TallGrassBlock : Block {
        public TallGrassBlock(BlockPosition position, BlockData data)
            : base(Identifiers.TallGrass, TallGrassBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<TallGrassBlockView>();
        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => true;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class TallGrassBlockType : BlockType {
            public static readonly TallGrassBlockType Instance = new TallGrassBlockType();

            private TallGrassBlockType() : base(
                Identifiers.TallGrass,
                "Tall Grass",
                new Aabb(0, 0, 0, 1, 1, 1),
                2,
                Resources.Load<Mesh>("Models/Blocks/TallGrass/Model"),
                Resources.Load<Texture>("Models/Blocks/TallGrass/Default")
            ) {
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new TallGrassBlock(position, data);
            }
        }
    }
}