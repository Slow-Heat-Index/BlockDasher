using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class BushBlock : Block {
        public BushBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Bush, BushBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<BushBlockView>();

        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class BushBlockType : BlockType {
            public static readonly BushBlockType Instance = new BushBlockType();

            private BushBlockType() : base(
                Identifiers.Bush,
                "Bush",
                new Aabb(0, 0, 0, 1, 1, 1),
                1,
                true,
                Resources.Load<Mesh>("Models/Blocks/Bush/Model"),
                Resources.Load<Texture>("Models/Blocks/Bush/Default")
            ) {
            }

            public override void EditEditorDisplay(GameObject obj, MeshFilter mesh, MeshRenderer renderer) {
                mesh.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new BushBlock(position, data);
            }
        }
    }
}