using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class ArmorBlock : Block {
        public ArmorBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Armor, ArmorBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<ArmorBlockView>();

        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class ArmorBlockType : BlockType {
            public static readonly ArmorBlockType Instance = new ArmorBlockType();

            private ArmorBlockType() : base(
                Identifiers.Armor,
                "Armor",
                new Aabb(0.2f, 0, 0.2f, 0.6f, 1, 0.6f),
                1,
                true,
                Resources.Load<Mesh>("Models/Blocks/Armor/Model"),
                Resources.Load<Texture>("Models/Blocks/Armor/Default")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataFacing.Key] = MetadataSnapshots.MetadataFacing;
            }

            public override void EditEditorDisplay(GameObject obj, MeshFilter mesh, MeshRenderer renderer) {
                mesh.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            }
            
            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new ArmorBlock(position, data);
            }
        }
    }
}