using Sources.Util;
using UnityEngine;

namespace Level.Blocks {
    public class TallGrassBlockView : CubicBlockView {
        protected override void Start() {
            base.Start();

            gameObject.isStatic = true;
            transform.localScale = new Vector3(1, 2, 1);
            transform.rotation = Quaternion.Euler(0, RotationHash(Block.Position.Position) * 90.0f, 0);
        }

        public override bool IsFaceOpaque(Direction direction) {
            return false;
        }

        protected override Mesh LoadMesh() {
            return Resources.Load<Mesh>("Models/Blocks/TallGrass/Model");
        }

        protected override Material LoadMaterial() {
            return Resources.Load<Material>("Models/Blocks/TallGrass/DefaultMaterial");
        }


        private int RotationHash(Vector3Int position) {
            return (position.x * 73 + position.y * 331 + position.z * 571) % 881;
        }
    }
}