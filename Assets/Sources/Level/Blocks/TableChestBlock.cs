using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class TableChestBlock : Block {
        public TableChestBlock(BlockPosition position, BlockData data)
            : base(Identifiers.TableChest, TableChestBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<TableChestBlockView>();

        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class TableChestBlockType : BlockType {
            public static readonly TableChestBlockType Instance = new TableChestBlockType();

            private TableChestBlockType() : base(
                Identifiers.TableChest,
                "Chest Table",
                new Aabb(0, 0, 0, 1, 1, 1),
                1,
                true,
                Resources.Load<Mesh>("Models/Blocks/TableChest/Model"),
                Resources.Load<Texture>("Models/Blocks/TableChest/Default")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataFacing.Key] = MetadataSnapshots.MetadataFacing;
            }

            public override void EditEditorDisplay(GameObject obj, MeshFilter mesh, MeshRenderer renderer) {
                mesh.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            }
            
            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new TableChestBlock(position, data);
            }
        }
    }
}