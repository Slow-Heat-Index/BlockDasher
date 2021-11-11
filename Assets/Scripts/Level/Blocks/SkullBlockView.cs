using System;
using Sources.Level;
using Sources.Level.Blocks;
using Sources.Util;
using UnityEngine;

namespace Level.Blocks {
    public class SkullBlockView : BlockView {
        private int _meshId;

        public override void Initialize() {
            _meshId = RotationHash(Block.Position.Position);

            base.Initialize();

            gameObject.isStatic = true;
            transform.rotation = Quaternion.Euler(0, _meshId * 90.0f, 0);
            transform.position += new Vector3(0, 0.24f, 0);
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
            var type = Block.GetMetadataEnum<SkullBlock.SkullType>(MetadataSnapshots.MetadataSkullType.Key, 0);
            var value = type == 0 ? _meshId % 2 : type - 1;
            
            return Resources.Load<Mesh>(value switch {
                0 => "Models/Blocks/Skull1/Model",
                1 => "Models/Blocks/Skull2/Model",
                _ => throw new ArgumentOutOfRangeException(value +" - "+_meshId)
            });
        }

        protected override Material LoadMaterial() {
            var type = Block.GetMetadataEnum<SkullBlock.SkullType>(MetadataSnapshots.MetadataSkullType.Key, 0);
            var value = type == 0 ? _meshId % 2 : type - 1;

            return Resources.Load<Material>(value switch {
                0 => "Models/Blocks/Skull1/DefaultMaterial",
                1 => "Models/Blocks/Skull2/DefaultMaterial",
                _ => throw new ArgumentOutOfRangeException(value +" - "+_meshId)
            });
        }


        private int RotationHash(Vector3Int position) {
            return Math.Abs(position.x * 73 + position.y * 331 + position.z * 571) % 881;
        }
    }
}