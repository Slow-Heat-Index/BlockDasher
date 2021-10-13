using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class TreeBlock : Block {
        public TreeBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Tree, TreeBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<TreeBlockView>();

        public override bool CanMoveTo(Direction direction) {
            return true;
        }

        public override bool CanMoveFrom(Direction direction) {
            return false;
        }

        public class TreeBlockType : BlockType {
            public static readonly TreeBlockType Instance = new TreeBlockType();

            private TreeBlockType() : base(
                Identifiers.Tree,
                "Tree",
                new Aabb(0.2f, 0, 0.2f, 0.6f, 1f, 0.6f),
                Resources.Load<Mesh>("Models/Props/Garden/Tree1/Model"),
                Resources.Load<Texture>("Models/Props/Garden/Tree1/Default")
            ) {
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new TreeBlock(position, data);
            }
        }
    }
}