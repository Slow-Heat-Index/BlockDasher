using Level;
using Level.Blocks;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Blocks {
    public class LabyrinthBushBlock : Block {
        public LabyrinthBushBlock(BlockPosition position, BlockData data)
            : base(Identifiers.LabyrinthBush, LabyrinthBushBlockType.Instance, position, data) {
        }

        public override Aabb CollisionBox {
            get {
                var dir = (Direction)GetMetadataEnum<Direction>(MetadataSnapshots.MetadataFacing.Key,
                    (int)Direction.North);
                return dir switch {
                    Direction.East => new Aabb(5 / 20.0f, 0, 0, 10 / 20.0f, 0.7f, 1),
                    Direction.West => new Aabb(5 / 20.0f, 0, 0, 10 / 20.0f, 0.7f, 1),
                    _ => new Aabb(0, 0, 5 / 20.0f, 1, 0.7f, 10 / 20.0f)
                };
            }
        }

        public override BlockView GenerateBlockView() => GameObject.AddComponent<LabyrinthBushBlockView>();

        public override bool CanMoveTo(Direction direction) => true;
        public override bool CanMoveFrom(Direction direction) => false;
        public override bool IsClimbableFrom(Direction direction) => false;

        public class LabyrinthBushBlockType : BlockType {
            public static readonly LabyrinthBushBlockType Instance = new LabyrinthBushBlockType();

            private LabyrinthBushBlockType() : base(
                Identifiers.LabyrinthBush,
                "Labyrinth Bush",
                new Aabb(0, 0, 0, 1, 1, 1),
                1,
                true,
                Resources.Load<Mesh>("Models/Blocks/LabyrinthBush/Model"),
                Resources.Load<Texture>("Models/Blocks/LabyrinthBush/Default")
            ) {
                DefaultMetadata[MetadataSnapshots.MetadataFacing.Key] = MetadataSnapshots.MetadataFacing;
            }

            public override void EditEditorDisplay(GameObject obj, MeshFilter mesh, MeshRenderer renderer) {
                mesh.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            }

            protected override Block CreateBlockImpl(BlockPosition position, BlockData data) {
                return new LabyrinthBushBlock(position, data);
            }
        }
    }
}