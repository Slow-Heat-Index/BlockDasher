using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class BeachGrassBlock : Block {
        public BeachGrassBlock(BlockPosition position, BlockData data)
            : base(Identifiers.BeachGrass, BeachGrassBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<BeachGrassBlockView>();
        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => true;

        public class BeachGrassBlockType : BlockType {
            public static readonly BeachGrassBlockType Instance = new BeachGrassBlockType();

            private BeachGrassBlockType() : base(
                Identifiers.BeachGrass,
                "Beach Grass",
                new Aabb(0, 0, 0, 1, 1, 1),
                2,
                false,
                Resources.Load<Mesh>("Models/BlockModel"),
                Resources.Load<Texture>("Models/Blocks/BeachGrass/White")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataForceTop.Key] = MetadataSnapshots.MetadataForceTop;
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new BeachGrassBlock(position, data);
            }
        }
    }
}