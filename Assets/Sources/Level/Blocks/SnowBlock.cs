using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class SnowBlock : Block {
        public SnowBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Snow, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<SnowBlockView>();

        public class BlockType : Level.BlockType {
            public static readonly BlockType Instance = new BlockType();

            private BlockType() : base(
                Identifiers.Snow,
                "Snow",
                Resources.Load<Mesh>("Models/BlockModel"),
                Resources.Load<Texture>("Models/Snow/White")
            ) {
            }

            public override Block CreateBlock(BlockPosition position, BlockData data) {
                return new SnowBlock(position, data);
            }
        }
    }
}