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
            Register(BricksBlock.BricksBlockType.Instance);
            Register(FenceBlock.FenceBlockType.Instance);
            Register(FenceCornerBlock.FenceCornerBlockType.Instance);
            Register(FlowersBlock.FlowersBlockType.Instance);
            Register(TreeBlock.TreeBlockType.Instance);
            Register(TallGrassBlock.TallGrassBlockType.Instance);
            Register(SignBlock.SignBlockType.Instance);
            Register(BushBlock.BushBlockType.Instance);
            Register(LabyrinthBushBlock.LabyrinthBushBlockType.Instance);
            Register(ArmorBlock.ArmorBlockType.Instance);
            Register(ArmorStandBlock.ArmorStandBlockType.Instance);
            Register(CauldronBlock.CauldronBlockType.Instance);
            Register(ChairBlock.ChairBlockType.Instance);
            Register(ClosetBlock.ClosetBlockType.Instance);
            Register(ChairFancyBlock.ChairFancyBlockType.Instance);
            Register(TableChestBlock.TableChestBlockType.Instance);
            Register(TableFruitsBlock.TableFruitsBlockType.Instance);
            Register(TableSwordsBlock.TableSwordsBlockType.Instance);
            Register(MirrorBlock.MirrorBlockType.Instance);
            Register(ShelvesBlock.ShelvesBlockType.Instance);
            Register(DeadBushBlock.DeadBushBlockType.Instance);
            Register(SkullBlock.SkullBlockType.Instance);
            Register(SandCastleBlock.SandCastleBlockType.Instance);
            Register(TallGrassDesertBlock.TallGrassDesertBlockType.Instance);
            Register(CactusBlock.CactusBlockType.Instance);
        }
    }
}