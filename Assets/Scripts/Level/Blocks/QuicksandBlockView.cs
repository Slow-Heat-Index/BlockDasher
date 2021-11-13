using Sources.Level;
using Sources.Util;
using UnityEngine;

namespace Level.Blocks {
    public class QuicksandBlockView : CubicBlockView {
        public override void Initialize() {
            base.Initialize();
            gameObject.isStatic = true;
        }

        protected override Mesh LoadMesh() {
            return Resources.Load<Mesh>("Models/BlockModel");
        }

        protected override Material LoadMaterial() {
            var position = Block.Position;

            var addition = position.Position.x + position.Position.z;
            return Resources.Load<Material>((addition & 1) == 0 
                ? "Models/Blocks/Quicksand/BlackMaterial"
                : "Models/Blocks/Quicksand/WhiteMaterial");
        }
    }
}