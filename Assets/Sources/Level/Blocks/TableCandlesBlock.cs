using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class TableCandlesBlock : Block {
        public TableCandlesBlock(BlockPosition position, BlockData data)
            : base(Identifiers.TableCandles, TableCandlesBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<TableCandlesBlockView>();

        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class TableCandlesBlockType : BlockType {
            public static readonly TableCandlesBlockType Instance = new TableCandlesBlockType();

            private TableCandlesBlockType() : base(
                Identifiers.TableCandles,
                "Candles Table",
                new Aabb(0, 0, 0, 1, 1, 1),
                1,
                true,
                Resources.Load<Mesh>("Models/Blocks/TableCandles/Model1"),
                Resources.Load<Texture>("Models/Blocks/TableCandles/Default")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataFacing.Key] = MetadataSnapshots.MetadataFacing;
                DefaultMetadata[MetadataSnapshots.MetadataInverse.Key] = MetadataSnapshots.MetadataInverse;
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new TableCandlesBlock(position, data);
            }
        }
    }
}