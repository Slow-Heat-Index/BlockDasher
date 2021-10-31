using Sources.Util;
using UnityEngine;

namespace Level.Blocks {
    public class EndBlockView : BlockView {
        public override void Initialize() {
            staticBlock = false;
            base.Initialize();
            gameObject.isStatic = true;
            transform.position += new Vector3(0, 0.001f, 0);
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
            return Resources.Load<Mesh>("Models/Blocks/End/Model");
        }

        protected override Material LoadMaterial() {
            return Resources.Load<Material>("Models/Blocks/End/DefaultMaterial");
        }
    }
}