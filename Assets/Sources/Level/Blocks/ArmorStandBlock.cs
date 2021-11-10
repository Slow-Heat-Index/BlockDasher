using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class ArmorStandBlock : Block {
        public ArmorStandBlock(BlockPosition position, BlockData data)
            : base(Identifiers.ArmorStand, ArmorStandBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<ArmorStandBlockView>();

        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class ArmorStandBlockType : BlockType {
            public static readonly ArmorStandBlockType Instance = new ArmorStandBlockType();

            private ArmorStandBlockType() : base(
                Identifiers.ArmorStand,
                "Armor Stand",
                new Aabb(0.2f, 0, 0.2f, 0.6f, 1, 0.6f),
                1,
                true,
                Resources.Load<Mesh>("Models/Blocks/ArmorStand/Model"),
                Resources.Load<Texture>("Models/Blocks/ArmorStand/Default")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataFacing.Key] = MetadataSnapshots.MetadataFacing;
            }

            public override void EditEditorDisplay(GameObject obj, MeshFilter mesh, MeshRenderer renderer) {
                mesh.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            }
            
            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new ArmorStandBlock(position, data);
            }
        }
    }
}