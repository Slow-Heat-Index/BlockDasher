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
        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class TreeBlockType : BlockType {
            public static readonly TreeBlockType Instance = new TreeBlockType();

            private TreeBlockType() : base(
                Identifiers.Tree,
                "Tree",
                new Aabb(0.2f, 0, 0.2f, 0.6f, 1f, 0.6f),
                2,
                true,
                Resources.Load<Mesh>("Models/Blocks/Tree1/Model"),
                Resources.Load<Texture>("Models/Blocks/Tree1/Default")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataTreeType.Key] = MetadataSnapshots.MetadataTreeType;
            }

            public override void EditEditorDisplay(GameObject obj, MeshFilter mesh, MeshRenderer renderer) {
                mesh.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new TreeBlock(position, data);
            }
        }


        public enum TreeType {
            Random,
            Normal,
            Grass,
            BigTree,
            SmallTree,
            PalmTree
        }
    }
}