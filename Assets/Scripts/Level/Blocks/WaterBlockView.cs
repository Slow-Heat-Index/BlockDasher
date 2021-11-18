using Sources.Util;
using UnityEngine;

namespace Level.Blocks {
    public class WaterBlockView : CubicBlockView {
        public override void Initialize() {
            base.Initialize();
            gameObject.isStatic = true;
        }

        protected override Mesh LoadMesh() {
            return Resources.Load<Mesh>("Models/BlockModel");
        }

        protected override Material LoadMaterial() {
            return Resources.Load<Material>("Models/Blocks/Water/DefaultMaterial");
        }

        public override bool IsFaceOpaque(Direction direction) {
            return false;
        }
    }
}