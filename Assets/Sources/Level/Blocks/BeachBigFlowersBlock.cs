using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class BeachBigFlowersBlock : Block {
        public BeachBigFlowersBlock(BlockPosition position, BlockData data)
            : base(Identifiers.BeachBigFlowers, BeachBigFlowersBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<BeachBigFlowersBlockView>();
        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class BeachBigFlowersBlockType : BlockType {
            public static readonly BeachBigFlowersBlockType Instance = new BeachBigFlowersBlockType();

            private BeachBigFlowersBlockType() : base(
                Identifiers.BeachBigFlowers,
                "Beach Big Flowers",
                new Aabb(0.1f, 0, 0.1f, 0.8f, 1f, 0.8f),
                2,
                true,
                Resources.Load<Mesh>("Models/Blocks/BeachFlowersRed/Model"),
                Resources.Load<Texture>("Models/Blocks/BeachFlowersRed/Default")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataBeachBigFlowersType.Key] = MetadataSnapshots.MetadataBeachBigFlowersType;
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new BeachBigFlowersBlock(position, data);
            }
        }


        public enum BeachBigFlowersType {
            RedFlower,
            RockFlower,
            Sunflower
        }
    }
}