using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class SandBlock : Block {
        public SandBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Sand, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<SandBlockView>();

        public class BlockType : Level.BlockType {
            public static readonly BlockType Instance = new BlockType();

            private BlockType() : base(
                Identifiers.Sand,
                "Sand",
                Resources.Load<Mesh>("Models/Grass/Model"),
                Resources.Load<Texture>("Models/Grass/Dirt")
            ) {
            }

            public override Block CreateBlock(BlockPosition position, BlockData data) {
                return new SandBlock(position, data);
            }
        }
    }
}