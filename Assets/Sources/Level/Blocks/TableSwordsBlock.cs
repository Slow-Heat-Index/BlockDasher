using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class TableSwordsBlock : Block {
        public TableSwordsBlock(BlockPosition position, BlockData data)
            : base(Identifiers.TableSwords, TableSwordsBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<TableSwordsBlockView>();

        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class TableSwordsBlockType : BlockType {
            public static readonly TableSwordsBlockType Instance = new TableSwordsBlockType();

            private TableSwordsBlockType() : base(
                Identifiers.TableSwords,
                "Sword Table",
                new Aabb(0.1f, 0, 0.1f, 0.8f, 0.8f, 0.8f),
                1,
                true,
                Resources.Load<Mesh>("Models/Blocks/TableSwords/Model"),
                Resources.Load<Texture>("Models/Blocks/TableSwords/Default")
            ) {
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new TableSwordsBlock(position, data);
            }
        }
    }
}