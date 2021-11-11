using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class SkullBlock : Block {
        public SkullBlock(BlockPosition position, BlockData data)
            : base(Identifiers.Skull, SkullBlockType.Instance, position, data) {
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<SkullBlockView>();
        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class SkullBlockType : BlockType {
            public static readonly SkullBlockType Instance = new SkullBlockType();

            private SkullBlockType() : base(
                Identifiers.Skull,
                "Skull",
                new Aabb(0.1f, 0, 0.1f, 0.8f, 0.5f, 0.8f),
                2,
                true,
                Resources.Load<Mesh>("Models/Blocks/Skull1/Model"),
                Resources.Load<Texture>("Models/Blocks/Skull1/Default")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataSkullType.Key] = MetadataSnapshots.MetadataSkullType;
            }

            public override void EditEditorDisplay(GameObject obj, MeshFilter mesh, MeshRenderer renderer) {
                mesh.transform.position += new Vector3(0, 0.4f, 0);
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new SkullBlock(position, data);
            }
        }
        
        public enum SkullType {
            Random,
            Normal,
            Broken
        }
    }
}