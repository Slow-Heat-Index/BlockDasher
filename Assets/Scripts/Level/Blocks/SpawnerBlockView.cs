using Level.Entities;
using Sources.Identification;
using Sources.Level;
using Sources.Registration;
using Sources.Util;
using UnityEngine;

namespace Level.Blocks {
    public class SpawnerBlockView : BlockView {
        private GameObject _child;

        public override void Initialize() {
            staticBlock = false;
            base.Initialize();
            gameObject.isStatic = true;

            if (!Block.Position.World.IsEditorWorld) return;

            var manager = Registry.Get<EntityType>(Identifiers.ManagerEntity);
            var id = Block.GetMetadata(MetadataSnapshots.MetadataEntityType.Key);
            var type = manager.Get(id == null ? Identifiers.Triangle : new Identifier(id));

            _child = Instantiate(type.GetSpawnerPrefab(), transform);
            _child.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            _child.transform.localPosition = new Vector3(0, 0.3f, 0);
            _child.transform.Rotate(-45, 0, 0);
        }

        protected override void Update() {
            base.Update();
            if (_child == null) return;
            _child.transform.Rotate(0, Time.deltaTime * 180, 0, Space.World);
        }


        public override bool IsFaceOpaque(Direction direction) {
            return false;
        }

        public override bool Collides(Direction fromFace, Vector3 current, Vector3 origin, Vector3 direction,
            out Direction face,
            out Vector3 collision) {
            return Block.CollisionBox.CollidesSegment(
                Block.Position.Position, current, current + direction * 2,
                out collision, out face);
        }

        protected override Mesh LoadMesh() {
            return !Block.Position.World.IsEditorWorld ? null : Resources.Load<Mesh>("Models/BlockModel");
        }

        protected override Material LoadMaterial() {
            return Resources.Load<Material>("Models/Blocks/Spawner/DefaultMaterial");
        }
    }
}