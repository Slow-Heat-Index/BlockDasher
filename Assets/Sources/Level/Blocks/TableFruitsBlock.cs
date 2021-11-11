using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class TableFruitsBlock : Block {
        public TableFruitsBlock(BlockPosition position, BlockData data)
            : base(Identifiers.TableFruits, TableFruitsBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<TableFruitsBlockView>();

        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class TableFruitsBlockType : BlockType {
            public static readonly TableFruitsBlockType Instance = new TableFruitsBlockType();

            private TableFruitsBlockType() : base(
                Identifiers.TableFruits,
                "Fruits Table",
                new Aabb(0, 0, 0, 1, 1, 1),
                1,
                true,
                Resources.Load<Mesh>("Models/Blocks/TableFruits/Model"),
                Resources.Load<Texture>("Models/Blocks/TableFruits/Default")
            ) {
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new TableFruitsBlock(position, data);
            }
        }
    }
}