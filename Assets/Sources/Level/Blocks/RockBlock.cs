using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class RockBlock : Block {
        public RockBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Rock, RockBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<RockBlockView>();
        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class RockBlockType : BlockType {
            public static readonly RockBlockType Instance = new RockBlockType();

            private RockBlockType() : base(
                Identifiers.Rock,
                "Rock",
                new Aabb(0, 0, 0, 1, 0.5f, 1),
                2,
                true,
                Resources.Load<Mesh>("Models/Blocks/Rock1/Model"),
                Resources.Load<Texture>("Models/Blocks/Rock1/Default")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataRockType.Key] = MetadataSnapshots.MetadataRockType;
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new RockBlock(position, data);
            }
        }


        public enum RockType {
            Random,
            Rock1,
            Rock2,
            Rock3
        }
    }
}