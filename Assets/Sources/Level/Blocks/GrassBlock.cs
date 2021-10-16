using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class GrassBlock : Block {
        public GrassBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Grass, GrassBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<GrassBlockView>();

        public override bool CanMoveTo(Direction direction) {
            return true;
        }

        public override bool CanMoveFrom(Direction direction) {
            return false;
        }
        
        public override bool IsClimbableFrom(Direction direction) {
            return true;
        }

        public class GrassBlockType : BlockType {
            public static readonly GrassBlockType Instance = new GrassBlockType();

            private GrassBlockType() : base(
                Identifiers.Grass,
                "Grass",
                new Aabb(0, 0, 0, 1, 1, 1),
                2,
                Resources.Load<Mesh>("Models/BlockModel"),
                Resources.Load<Texture>("Models/Grass/White")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataForceTop.Key] = MetadataSnapshots.MetadataForceTop;
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new GrassBlock(position, data);
            }
        }
    }
}