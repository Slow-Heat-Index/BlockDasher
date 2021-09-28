using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;

namespace Sources.Level.Blocks {
    public class GrassBlock : Block {
        public GrassBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Grass, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<GrassBlockView>();

        public class BlockType : Level.BlockType {
            public BlockType() : base(Identifiers.Grass) {
            }

            public override Block CreateBlock(BlockPosition position, BlockData data) {
                return new GrassBlock(position, data);
            }
        }
    }
}