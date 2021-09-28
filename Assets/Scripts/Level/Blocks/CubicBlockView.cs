using Sources.Util;
using UnityEngine;

namespace Level.Blocks {
    public abstract class CubicBlockView : BlockView {

        public override bool Collides(Direction fromFace, Vector3 current, Vector3 origin, Vector3 direction,
            out Direction face, out Vector3 collision) {
            face = fromFace;
            collision = current;
            return true;
        }

        public override bool IsFaceOpaque(Direction direction) {
            return true;
        }
    }
}