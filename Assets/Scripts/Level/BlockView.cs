using Sources.Level;
using Sources.Util;
using UnityEngine;

namespace Level {
    public abstract class BlockView : MonoBehaviour {
        public Block Block;
        public MeshRenderer Renderer;

        private bool _visibilityDirty = true;

        protected virtual void Start() {
            var filter = gameObject.AddComponent<MeshFilter>();
            filter.mesh = LoadMesh();
            Renderer = gameObject.AddComponent<MeshRenderer>();
            Renderer.material = LoadMaterial();
            transform.position += new Vector3(0.5f, 0, 0.5f);
        }

        protected virtual void Update() {
            if (_visibilityDirty) {
                RefreshVisibility();
                _visibilityDirty = false;
            }
        }

        public void MarkVisibilityDirty() {
            _visibilityDirty = true;
        }

        public abstract bool IsFaceOpaque(Direction direction);

        public abstract bool Collides(Direction fromFace, Vector3 current, Vector3 origin, Vector3 direction,
            out Direction face, out Vector3 collision);

        protected void RefreshVisibility() {
            var position = Block.Position;
            var count = 0;
            DirectionUtils.ForEach(direction => {
                var pos = position;
                pos.Move(direction);
                var block = pos.Block;
                if (block != null && block.View.IsFaceOpaque(direction.GetOpposite())) count++;
            });
            Renderer.enabled = count < 6;
        }

        protected abstract Mesh LoadMesh();
        protected abstract Material LoadMaterial();
    }
}