using UnityEngine;

namespace Level.Blocks {
    public class SandBlockView : CubicBlockView {
        protected override void Start() {
            base.Start();
            gameObject.isStatic = true;
        }

        protected override Mesh LoadMesh() {
            return Resources.Load<Mesh>("Models/Grass/Model");
        }

        protected override Material LoadMaterial() {
            return Resources.Load<Material>("Models/Grass/DirtMaterial");
        }
    }
}