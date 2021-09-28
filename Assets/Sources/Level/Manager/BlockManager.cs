using Sources.Identification;
using Sources.Level.Blocks;
using Sources.Registration;

namespace Sources.Level.Manager {
    public class BlockManager : Manager<Block.Builder> {
        public BlockManager() : base(Identifiers.ManagerBlock) {
            Register(new GrassBlock.Builder());
        }
    }
}