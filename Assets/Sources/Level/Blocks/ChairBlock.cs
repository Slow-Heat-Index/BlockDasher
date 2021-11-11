using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class ChairBlock : Block {
        public ChairBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Chair, ChairBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<ChairBlockView>();

        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class ChairBlockType : BlockType {
            public static readonly ChairBlockType Instance = new ChairBlockType();

            private ChairBlockType() : base(
                Identifiers.Chair,
                "Chair",
                new Aabb(0.2f, 0, 0.2f, 0.6f, 1, 0.6f),
                1,
                true,
                Resources.Load<Mesh>("Models/Blocks/Chair/Model"),
                Resources.Load<Texture>("Models/Blocks/Chair/Default")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataFacing.Key] = MetadataSnapshots.MetadataFacing;
            }

            public override void EditEditorDisplay(GameObject obj, MeshFilter mesh, MeshRenderer renderer) {
                mesh.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            }
            
            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new ChairBlock(position, data);
            }
        }
    }
}