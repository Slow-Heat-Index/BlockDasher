using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class LabyrinthCornerBushBlock : Block {
        public LabyrinthCornerBushBlock(BlockPosition position, BlockData data)
            : base(Identifiers.LabyrinthCornerBush, LabyrinthCornerBushBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<LabyrinthCornerBushBlockView>();

        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class LabyrinthCornerBushBlockType : BlockType {
            public static readonly LabyrinthCornerBushBlockType Instance = new LabyrinthCornerBushBlockType();

            private LabyrinthCornerBushBlockType() : base(
                Identifiers.LabyrinthCornerBush,
                "Labyrinth Corner Bush",
                new Aabb(0, 0, 0, 1, 1, 1),
                1,
                true,
                Resources.Load<Mesh>("Models/Blocks/LabyrinthCornerBush/Model"),
                Resources.Load<Texture>("Models/Blocks/LabyrinthCornerBush/Default")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataFacing.Key] = MetadataSnapshots.MetadataFacing;
            }

            public override void EditEditorDisplay(GameObject obj, MeshFilter mesh, MeshRenderer renderer) {
                mesh.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new LabyrinthCornerBushBlock(position, data);
            }
        }
    }
}