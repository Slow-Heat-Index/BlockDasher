using Sources.Level;
using Sources.Level.Blocks;
using Sources.Util;
using UnityEngine;

namespace Level.Blocks {
    public class GrassBlockView : CubicBlockView {
        public override void Initialize() {
            base.Initialize();
            gameObject.isStatic = true;
        }

        protected override Mesh LoadMesh() {
            return Resources.Load<Mesh>("Models/BlockModel");
        }

        protected override Material LoadMaterial() {
            var position = Block.Position;
            var up = position.Moved(Direction.Up);

            var force = Block.GetMetadataBoolean(MetadataSnapshots.MetadataForceTop.Key);

            var upBlock = up.Block;
            if (!force && upBlock != null && upBlock.View.IsFaceOpaque(Direction.Down)) {
                return Resources.Load<Material>("Models/Blocks/Grass/DirtMaterial");
            }

            var addition = position.Position.x + position.Position.z;
            if ((addition & 1) == 0) {
                return Resources.Load<Material>("Models/Blocks/Grass/BlackMaterial");
            }

            return Resources.Load<Material>("Models/Blocks/Grass/WhiteMaterial");
        }
    }
}