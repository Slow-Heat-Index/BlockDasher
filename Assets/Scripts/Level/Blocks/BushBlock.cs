using System;
using Sources.Level;
using Sources.Util;
using UnityEngine;

namespace Level.Blocks {
    public class BushBlockView : BlockView {
        private int _meshId;

        public override void Initialize() {
            _meshId = RotationHash(Block.Position.Position);

            base.Initialize();

            gameObject.isStatic = true;
            transform.rotation = Quaternion.Euler(0, _meshId * 90.0f, 0);
        }

        public override bool IsFaceOpaque(Direction direction) => false;

        public override bool Collides(Direction fromFace, Vector3 current, Vector3 origin, Vector3 direction,
            out Direction face,
            out Vector3 collision) {
            return Block.CollisionBox.CollidesSegment(
                Block.Position.Position, current, current + direction * 2,
                out collision, out face);
        }

        protected override Mesh LoadMesh() {
            return Resources.Load<Mesh>("Models/Blocks/Bush/Model");
        }

        protected override Material LoadMaterial() {
            return Resources.Load<Material>("Models/Blocks/Bush/DefaultMaterial");
        }
        
        private int RotationHash(Vector3Int position) {
            return Math.Abs(position.x * 73 + position.y * 331 + position.z * 571) % 881;
        }
    }
}