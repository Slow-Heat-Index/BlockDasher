using Sources.Level;
using Sources.Util;
using UnityEngine;

namespace Level.Blocks {
    public class TableFruitsBlockView : BlockView {
        public override void Initialize() {
            base.Initialize();
            gameObject.isStatic = true;
            transform.position -= new Vector3(0.02f, 0, 0);
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
            return Resources.Load<Mesh>("Models/Blocks/TableFruits/Model");
        }

        protected override Material LoadMaterial() {
            return Resources.Load<Material>("Models/Blocks/TableFruits/DefaultMaterial");
        }
    }
}