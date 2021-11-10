using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class ChairFancyBlock : Block {
        public ChairFancyBlock(BlockPosition position, BlockData data)
            : base(Identifiers.ChairFancy, ChairFancyBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<ChairFancyBlockView>();

        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class ChairFancyBlockType : BlockType {
            public static readonly ChairFancyBlockType Instance = new ChairFancyBlockType();

            private ChairFancyBlockType() : base(
                Identifiers.ChairFancy,
                "Chair Fancy",
                new Aabb(0, 0, 0, 1, 1, 1),
                1,
                true,
                Resources.Load<Mesh>("Models/Blocks/ChairFancy/Model"),
                Resources.Load<Texture>("Models/Blocks/ChairFancy/Default")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataFacing.Key] = MetadataSnapshots.MetadataFacing;
            }

            public override void EditEditorDisplay(GameObject obj, MeshFilter mesh, MeshRenderer renderer) {
                mesh.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            }
            
            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new ChairFancyBlock(position, data);
            }
        }
    }
}