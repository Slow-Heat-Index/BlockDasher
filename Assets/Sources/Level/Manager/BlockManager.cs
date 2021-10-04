using Sources.Identification;
using Sources.Level.Blocks;
using Sources.Registration;

namespace Sources.Level.Manager {
    public class BlockManager : Manager<BlockType> {
        public BlockManager() : base(Identifiers.ManagerBlock) {
            Register(GrassBlock.BlockType.Instance);
            Register(SandBlock.BlockType.Instance);
            Register(SnowBlock.BlockType.Instance);
        }
    }
}