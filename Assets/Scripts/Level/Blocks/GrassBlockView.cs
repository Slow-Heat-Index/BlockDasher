using Sources.Util;
using UnityEngine;

namespace Level.Blocks {
    public class GrassBlockView : CubicBlockView {
        protected override void Start() {
            base.Start();
            gameObject.isStatic = true;
        }

        protected override Mesh LoadMesh() {
            return Resources.Load<Mesh>("Models/BlockModel");
        }

        protected override Material LoadMaterial() {
            return Resources.Load<Material>("Models/Grass/DefaultMaterial");
        }
    }
}