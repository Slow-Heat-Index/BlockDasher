using System;
using Sources.Level;
using Sources.Level.Blocks;
using Sources.Util;
using UnityEngine;

namespace Level.Blocks {
    public class TreeBlockView : BlockView {
        private int _meshId;

        public override void Initialize() {
            _meshId = RotationHash(Block.Position.Position);

            base.Initialize();

            gameObject.isStatic = true;
            transform.rotation = Quaternion.Euler(0, _meshId * 90.0f, 0);
        }

        public override bool IsFaceOpaque(Direction direction) {
            return false;
        }

        public override bool Collides(Direction fromFace, Vector3 current, Vector3 origin, Vector3 direction,
            out Direction face,
            out Vector3 collision) {
            return Block.BlockType.DefaultCollisionBox.CollidesSegment(
                Block.Position.Position, current, current + direction * 2,
                out collision, out face);
        }

        protected override Mesh LoadMesh() {
            var type = Block.GetMetadataEnum<TreeBlock.TreeType>(MetadataSnapshots.MetadataTreeType.Key, 0);
            var value = type == 0 ? _meshId % 4 : type - 1;
            
            return Resources.Load<Mesh>(value switch {
                0 => "Models/Blocks/Tree1/Model",
                1 => "Models/Blocks/Tree2/Model",
                2 => "Models/Blocks/Tree3/Model",
                3 => "Models/Blocks/Tree4/Model",
                4 => "Models/Blocks/PalmTree/Model",
                _ => throw new ArgumentOutOfRangeException(value +" - "+_meshId)
            });
        }

        protected override Material LoadMaterial() {
            var type = Block.GetMetadataEnum<TreeBlock.TreeType>(MetadataSnapshots.MetadataTreeType.Key, 0);
            var value = type == 0 ? _meshId % 4 : type - 1;

            return Resources.Load<Material>(value switch {
                0 => "Models/Blocks/Tree1/DefaultMaterial",
                1 => "Models/Blocks/Tree2/DefaultMaterial",
                2 => "Models/Blocks/Tree3/DefaultMaterial",
                3 => "Models/Blocks/Tree4/DefaultMaterial",
                4 => "Models/Blocks/PalmTree/DefaultMaterial",
                _ => throw new ArgumentOutOfRangeException(value +" - "+_meshId)
            });
        }


        private int RotationHash(Vector3Int position) {
            return Math.Abs(position.x * 73 + position.y * 331 + position.z * 571) % 881;
        }
    }
}