using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class ChestTableBlock : Block {
        public ChestTableBlock(BlockPosition position, BlockData data)
            : base(Identifiers.ChestTable, ChestTableBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<ChestTableBlockView>();

        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class ChestTableBlockType : BlockType {
            public static readonly ChestTableBlockType Instance = new ChestTableBlockType();

            private ChestTableBlockType() : base(
                Identifiers.ChestTable,
                "Chest Table",
                new Aabb(0.2f, 0, 0.2f, 0.6f, 1, 0.6f),
                1,
                true,
                Resources.Load<Mesh>("Models/Blocks/ChestTable/Model"),
                Resources.Load<Texture>("Models/Blocks/ChestTable/Default")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataFacing.Key] = MetadataSnapshots.MetadataFacing;
            }

            public override void EditEditorDisplay(GameObject obj, MeshFilter mesh, MeshRenderer renderer) {
                mesh.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            }
            
            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new ChestTableBlock(position, data);
            }
        }
    }
}