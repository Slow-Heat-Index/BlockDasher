using Sources.Level;
using Sources.Util;
using UnityEngine;

namespace Level.Blocks {
    public class MirrorBlockView : BlockView {
        public override void Initialize() {
            base.Initialize();
            gameObject.isStatic = true;

            var facing = (Direction)Block.GetMetadataEnum<Direction>(MetadataSnapshots.MetadataFacing.Key,
                (int)Direction.North);
            gameObject.transform.rotation = Quaternion.LookRotation(facing.GetVector());
        }

        public override bool IsFaceOpaque(Direction direction) => false;

        public override bool Collides(Direction fromFace, Vector3 current, Vector3 origin, Vector3 direction,
            out Direction face,
            out Vector3 collision) {
            return Block.CollisionBox.CollidesSegment(
                Block.Position.Position, current, current + direction * 2,
                out collision, out face);
        }

        protected override Mesh LoadMesh() {
            return Resources.Load<Mesh>("Models/Blocks/Mirror/Model");
        }

        protected override Material LoadMaterial() {
            return Resources.Load<Material>("Models/Blocks/Mirror/DefaultMaterial");
        }
    }
}