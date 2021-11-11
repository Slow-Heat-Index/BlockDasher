using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class SandCastleBlock : Block {
        public SandCastleBlock(BlockPosition position, BlockData data)
            : base(Identifiers.SandCastle, SandCastleBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<SandCastleBlockView>();

        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class SandCastleBlockType : BlockType {
            public static readonly SandCastleBlockType Instance = new SandCastleBlockType();

            private SandCastleBlockType() : base(
                Identifiers.SandCastle,
                "Sand Castle",
                new Aabb(0.1f, 0, 0.1f, 0.8f, 0.6f, 0.8f),
                1,
                true,
                Resources.Load<Mesh>("Models/Blocks/SandCastle/Model"),
                Resources.Load<Texture>("Models/Blocks/SandCastle/Default")
            ) {
            }

            public override void EditEditorDisplay(GameObject obj, MeshFilter mesh, MeshRenderer renderer) {
                mesh.transform.position += new Vector3(0, 0.4f, 0);
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new SandCastleBlock(position, data);
            }
        }
    }
}