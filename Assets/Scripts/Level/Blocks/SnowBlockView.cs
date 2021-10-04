using Sources.Util;
using UnityEngine;

namespace Level.Blocks {
    public class SnowBlockView : CubicBlockView {
        protected override void Start() {
            base.Start();
            gameObject.isStatic = true;
        }

        protected override Mesh LoadMesh() {
            return Resources.Load<Mesh>("Models/BlockModel");
        }

        protected override Material LoadMaterial() {
            var position = Block.Position;
            var up = position;
            up.Move(Direction.Up);

            var upBlock = up.Block;

            if (upBlock != null && upBlock.View.IsFaceOpaque(Direction.Down)) {
                return Resources.Load<Material>("Models/Snow/DirtMaterial");
            }

            var addition = position.Position.x + position.Position.z;
            if ((addition & 1) == 0) {
                
                return Resources.Load<Material>("Models/Snow/BlackMaterial");
                
            }

            return Resources.Load<Material>("Models/Snow/WhiteMaterial");
        }
    }
}