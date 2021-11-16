using Sources.Identification;
using Sources.Level.Blocks;
using Sources.Registration;

namespace Sources.Level.Manager {
    public class BlockManager : Manager<BlockType> {
        public BlockManager() : base(Identifiers.ManagerBlock) {
            Register(StartBlock.StartBlockType.Instance);
            Register(EndBlock.EndBlockType.Instance);
            Register(SpawnerBlock.SpawnerBlockType.Instance);
            Register(GrassBlock.GrassBlockType.Instance);
            Register(SandBlock.SandBlockType.Instance);
            Register(WetSandBlock.WetSandBlockType.Instance);
            Register(QuicksandBlock.QuicksandBlockType.Instance);
            Register(SnowBlock.SnowBlockType.Instance);
            Register(BricksBlock.BricksBlockType.Instance);
            Register(BeachGrassBlock.BeachGrassBlockType.Instance);
            Register(BeachSandBlock.BeachSandBlockType.Instance);
            Register(WaterBlock.WaterBlockType.Instance);
            Register(FenceBlock.FenceBlockType.Instance);
            Register(FenceCornerBlock.FenceCornerBlockType.Instance);
            Register(FlowersBlock.FlowersBlockType.Instance);
            Register(TreeBlock.TreeBlockType.Instance);
            Register(TallGrassBlock.TallGrassBlockType.Instance);
            Register(SignBlock.SignBlockType.Instance);
            Register(BushBlock.BushBlockType.Instance);
            Register(LabyrinthBushBlock.LabyrinthBushBlockType.Instance);
            Register(LabyrinthCornerBushBlock.LabyrinthCornerBushBlockType.Instance);
            Register(ArmorBlock.ArmorBlockType.Instance);
            Register(ArmorStandBlock.ArmorStandBlockType.Instance);
            Register(CauldronBlock.CauldronBlockType.Instance);
            Register(ChairBlock.ChairBlockType.Instance);
            Register(ClosetBlock.ClosetBlockType.Instance);
            Register(ChairFancyBlock.ChairFancyBlockType.Instance);
            Register(TableChestBlock.TableChestBlockType.Instance);
            Register(TableFruitsBlock.TableFruitsBlockType.Instance);
            Register(TableSwordsBlock.TableSwordsBlockType.Instance);
            Register(TableCandlesBlock.TableCandlesBlockType.Instance);
            Register(MirrorBlock.MirrorBlockType.Instance);
            Register(ShelvesBlock.ShelvesBlockType.Instance);
            Register(BenchBlock.BenchBlockType.Instance);
            Register(DeadBushBlock.DeadBushBlockType.Instance);
            Register(SkullBlock.SkullBlockType.Instance);
            Register(SandCastleBlock.SandCastleBlockType.Instance);
            Register(TallGrassDesertBlock.TallGrassDesertBlockType.Instance);
            Register(CactusBlock.CactusBlockType.Instance);
            Register(CoconutBlock.CoconutBlockType.Instance);
            Register(RockBlock.RockBlockType.Instance);
            Register(BeachFlowersBlock.BeachFlowersBlockType.Instance);
            Register(BeachBigFlowersBlock.BeachBigFlowersBlockType.Instance);
            Register(LianaBlock.LianaBlockType.Instance);
            Register(LilyPadBlock.LilyPadBlockType.Instance);
        }
    }
}