using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class BeachFlowersBlock : Block {
        public BeachFlowersBlock(BlockPosition position, BlockData data)
            : base(Identifiers.BeachFlowers, BeachFlowersBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<BeachFlowersBlockView>();
        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => true;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class BeachFlowersBlockType : BlockType {
            public static readonly BeachFlowersBlockType Instance = new BeachFlowersBlockType();

            private BeachFlowersBlockType() : base(
                Identifiers.BeachFlowers,
                "Beach Flowers",
                new Aabb(0.2f, 0, 0.2f, 0.6f, 0.25f, 0.6f),
                2,
                true,
                Resources.Load<Mesh>("Models/Blocks/BeachFlowers1/Model"),
                Resources.Load<Texture>("Models/Blocks/BeachFlowers1/Default")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataBeachFlowersType.Key] = MetadataSnapshots.MetadataBeachFlowersType;
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new BeachFlowersBlock(position, data);
            }
        }


        public enum BeachFlowersType {
            Flowers1,
            Flowers2,
            Flowers3
        }
    }
}