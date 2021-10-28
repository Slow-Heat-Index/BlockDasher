using Sources.Util;
using UnityEngine;

namespace Level.Blocks {
    public class FlowersBlockView : BlockView {
        public override void Initialize() {
            base.Initialize();
            gameObject.isStatic = true;
            transform.rotation = Quaternion.Euler(0, RotationHash(Block.Position.Position) * 90.0f, 0);
        }

        public override bool IsFaceOpaque(Direction direction) {
            return false;
        }

        public override bool Collides(Direction fromFace, Vector3 current, Vector3 origin, Vector3 direction,
            out Direction face,
            out Vector3 collision) {
            return Block.BlockType.DefaultCollisionBox.CollidesSegment(
                Block.Position.Position, current, current + direction * 2,
                out collision, out face);
        }

        protected override Mesh LoadMesh() {
            return Resources.Load<Mesh>((RotationHash(Block.Position.Position) & 0x4) > 0
                ? "Models/Blocks/Flowers1/Model"
                : "Models/Blocks/Flowers2/Model"
            );
        }

        protected override Material LoadMaterial() {
            return Resources.Load<Material>((RotationHash(Block.Position.Position) & 0x4) > 0
                ? "Models/Blocks/Flowers1/DefaultMaterial"
                : "Models/Blocks/Flowers2/DefaultMaterial"
            );
        }


        private int RotationHash(Vector3Int position) {
            return (position.x * 73 + position.y * 331 + position.z * 571) % 881;
        }
    }
}