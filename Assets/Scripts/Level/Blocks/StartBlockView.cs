using Sources.Util;
using UnityEngine;

namespace Level.Blocks {
    public class StartBlockView : BlockView {
        protected override void Start() {
            base.Start();
            gameObject.isStatic = true;
            transform.position += new Vector3(0, 0.001f, 0);
        }

        public override bool IsFaceOpaque(Direction direction) {
            return false;
        }

        public override bool Collides(Direction fromFace, Vector3 current, Vector3 origin, Vector3 direction,
            out Direction face,
            out Vector3 collision) {
            return Block.CollisionBox.CollidesSegment(
                Block.Position.Position, current, current + direction * 2,
                out collision, out face);
        }

        protected override Mesh LoadMesh() {
            return !Block.Position.World.IsEditorWorld ? null : Resources.Load<Mesh>("Models/StartEndModel");
        }

        protected override Material LoadMaterial() {
            return Resources.Load<Material>("Models/Blocks/Start/DefaultMaterial");
        }
    }
}