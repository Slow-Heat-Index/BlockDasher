using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class ClosetBlock : Block {
        public ClosetBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Closet, ClosetBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<ClosetBlockView>();

        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class ClosetBlockType : BlockType {
            public static readonly ClosetBlockType Instance = new ClosetBlockType();

            private ClosetBlockType() : base(
                Identifiers.Closet,
                "Closet",
                new Aabb(0,0,0,1,1,1),
                1,
                true,
                Resources.Load<Mesh>("Models/Blocks/Closet/Model"),
                Resources.Load<Texture>("Models/Blocks/Closet/Default")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataFacing.Key] = MetadataSnapshots.MetadataFacing;
            }

            public override void EditEditorDisplay(GameObject obj, MeshFilter mesh, MeshRenderer renderer) {
                mesh.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            }
            
            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new ClosetBlock(position, data);
            }
        }
    }
}