using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class ShelvesBlock : Block {
        public ShelvesBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Shelves, ShelvesBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<ShelvesBlockView>();

        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class ShelvesBlockType : BlockType {
            public static readonly ShelvesBlockType Instance = new ShelvesBlockType();

            private ShelvesBlockType() : base(
                Identifiers.Shelves,
                "Shelves",
                new Aabb(0, 0, 0, 1, 1, 1),
                1,
                true,
                Resources.Load<Mesh>("Models/Blocks/Shelves/Model"),
                Resources.Load<Texture>("Models/Blocks/Shelves/Default")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataFacing.Key] = MetadataSnapshots.MetadataFacing;
            }

            public override void EditEditorDisplay(GameObject obj, MeshFilter mesh, MeshRenderer renderer) {
                mesh.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new ShelvesBlock(position, data);
            }
        }
    }
}