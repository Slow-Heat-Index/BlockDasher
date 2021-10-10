using Sources.Identification;
using Sources.Level.Blocks;
using Sources.Registration;

namespace Sources.Level.Manager {
    public class BlockManager : Manager<BlockType> {
        public BlockManager() : base(Identifiers.ManagerBlock) {
            Register(StartBlock.StartBlockType.Instance);
            Register(EndBlock.EndBlockType.Instance);
            Register(GrassBlock.GrassBlockType.Instance);
            Register(SandBlock.SandBlockType.Instance);
            Register(SnowBlock.SnowBlockType.Instance);
            Register(FlowersBlock.FlowersBlockType.Instance);
            Register(TreeBlock.TreeBlockType.Instance);
        }
    }
}