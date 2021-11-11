using System;
using Sources.Util;
using UnityEngine;

namespace Level.Blocks {
    public class TallGrassDesertBlockView : CubicBlockView {
        public override void Initialize() {
            base.Initialize();

            gameObject.isStatic = true;
            transform.rotation = Quaternion.Euler(0, RotationHash(Block.Position.Position) * 90.0f, 0);
            transform.position += new Vector3(0, 0.102f, 0);
        }

        public override bool IsFaceOpaque(Direction direction) {
            return false;
        }

        protected override Mesh LoadMesh() {
            return Resources.Load<Mesh>("Models/Blocks/TallGrassDesert/Model");
        }

        protected override Material LoadMaterial() {
            return Resources.Load<Material>("Models/Blocks/TallGrassDesert/DefaultMaterial");
        }


        private int RotationHash(Vector3Int position) {
            return Math.Abs(position.x * 73 + position.y * 331 + position.z * 571) % 881;
        }
    }
}