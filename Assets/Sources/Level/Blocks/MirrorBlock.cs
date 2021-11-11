using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class MirrorBlock : Block {
        public MirrorBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Mirror, MirrorBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<MirrorBlockView>();

        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class MirrorBlockType : BlockType {
            public static readonly MirrorBlockType Instance = new MirrorBlockType();

            private MirrorBlockType() : base(
                Identifiers.Mirror,
                "Mirror",
                new Aabb(0.1f, 0, 0.1f, 0.8f, 1, 0.8f),
                1,
                true,
                Resources.Load<Mesh>("Models/Blocks/Mirror/Model"),
                Resources.Load<Texture>("Models/Blocks/Mirror/Default")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataFacing.Key] = MetadataSnapshots.MetadataFacing;
            }

            public override void EditEditorDisplay(GameObject obj, MeshFilter mesh, MeshRenderer renderer) {
                mesh.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new MirrorBlock(position, data);
            }
        }
    }
}