using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class DeadBushBlock : Block {
        public DeadBushBlock(BlockPosition position, BlockData data)
            : base(Identifiers.DeadBush, DeadBushBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<DeadBushBlockView>();

        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class DeadBushBlockType : BlockType {
            public static readonly DeadBushBlockType Instance = new DeadBushBlockType();

            private DeadBushBlockType() : base(
                Identifiers.DeadBush,
                "Dead Bush",
                new Aabb(0.2f, 0, 0.2f, 0.6f, 1, 0.6f),
                1,
                true,
                Resources.Load<Mesh>("Models/Blocks/DeadBush/Model"),
                Resources.Load<Texture>("Models/Blocks/DeadBush/Default")
            ) {
            }

            public override void EditEditorDisplay(GameObject obj, MeshFilter mesh, MeshRenderer renderer) {
                mesh.transform.position += new Vector3(0, 0.4f, 0);
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new DeadBushBlock(position, data);
            }
        }
    }
}