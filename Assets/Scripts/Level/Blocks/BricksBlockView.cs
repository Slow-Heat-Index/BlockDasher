using System;
using Sources.Level;
using Sources.Util;
using UnityEngine;

namespace Level.Blocks {
    public class BricksBlockView : CubicBlockView {
        public override void Initialize() {
            base.Initialize();
            gameObject.isStatic = true;
        }

        protected override Mesh LoadMesh() {
            return Resources.Load<Mesh>("Models/BlockFacesModel");
        }

        protected override Material LoadMaterial() {
            var position = Block.Position;
            var hash = TypeHash(position.Position);
            var up = position.Moved(Direction.Up);
            var force = Block.GetMetadataBoolean(MetadataSnapshots.MetadataForceTop.Key);

            var upBlock = up.Block;
            if (!force && upBlock != null && upBlock.View.IsFaceOpaque(Direction.Down)) {
                return Resources.Load<Material>((hash & 0x8) == 0
                    ? "Models/Blocks/Bricks/Bottom1Material"
                    : "Models/Blocks/Bricks/Bottom2Material");
            }

            return Resources.Load<Material>((hash & 0x8) == 0
                ? "Models/Blocks/Bricks/Top1Material"
                : "Models/Blocks/Bricks/Top2Material");
        }

        private int TypeHash(Vector3Int position) {
            return Math.Abs(position.x * 73 + position.y * 331 + position.z * 571) % 881;
        }
    }
}