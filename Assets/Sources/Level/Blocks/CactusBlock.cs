using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class CactusBlock : Block {
        public CactusBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Cactus, CactusBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<CactusBlockView>();

        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class CactusBlockType : BlockType {
            public static readonly CactusBlockType Instance = new CactusBlockType();

            private CactusBlockType() : base(
                Identifiers.Cactus,
                "Cactus",
                new Aabb(0.1f, 0, 0.1f, 0.8f, 0.6f, 0.8f),
                1,
                true,
                Resources.Load<Mesh>("Models/Blocks/Cactus/Model"),
                Resources.Load<Texture>("Models/Blocks/Cactus/Default")
            ) {
            }

            public override void EditEditorDisplay(GameObject obj, MeshFilter mesh, MeshRenderer renderer) {
                mesh.transform.position += new Vector3(0, 0.46f, 0);
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new CactusBlock(position, data);
            }
        }
    }
}