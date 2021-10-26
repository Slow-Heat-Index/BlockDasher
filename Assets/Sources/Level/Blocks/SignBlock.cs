using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class SignBlock : Block {
        public SignBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Sign, SignBlockType.Instance, position, data) {
        }
        
        public override BlockView GenerateBlockView() => GameObject.AddComponent<SignBlockView>();

        public override bool CanMoveTo(Direction direction) => true;

        public override bool CanMoveFrom(Direction direction) => false;

        public override bool IsClimbableFrom(Direction direction) => false;

        public class SignBlockType : BlockType {
            public static readonly SignBlockType Instance = new SignBlockType();

            private SignBlockType() : base(
                Identifiers.Sign,
                "Sign",
                new Aabb(0, 0, 0, 1, 1, 1),
                1,
                Resources.Load<Mesh>("Models/Blocks/Sign/Model"),
                Resources.Load<Texture>("Models/Blocks/Sign/Default")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataFacing.Key] = MetadataSnapshots.MetadataFacing;
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new SignBlock(position, data);
            }
        }
    }
}