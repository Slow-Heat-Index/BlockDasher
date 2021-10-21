using Sources.Level;
using Sources.Util;
using UnityEngine;

namespace Level.Blocks {
    public class FenceCornerBlockView : BlockView {
        protected override void Start() {
            base.Start();
            gameObject.isStatic = true;

            var facing = (Direction)Block.GetMetadataEnum<Direction>(MetadataSnapshots.MetadataFacing.Key,
                (int)Direction.North);
            gameObject.transform.rotation = Quaternion.LookRotation(facing.GetVector());
        }

        public override bool IsFaceOpaque(Direction direction) => false;

        public override bool Collides(Direction fromFace, Vector3 current, Vector3 origin, Vector3 direction,
            out Direction face,
            out Vector3 collision) {
            face = fromFace;
            collision = current;
            return true;
        }

        protected override Mesh LoadMesh() {
            return Resources.Load<Mesh>("Models/Blocks/FenceCorner/Model");
        }

        protected override Material LoadMaterial() {
            return Resources.Load<Material>("Models/Blocks/FenceCorner/DefaultMaterial");
        }
    }
}