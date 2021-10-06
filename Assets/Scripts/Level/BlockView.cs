using Sources.Level;
using Sources.Util;
using UnityEngine;

namespace Level {
    public abstract class BlockView : MonoBehaviour {
        public Block Block;

        protected MeshFilter Filter;
        protected MeshRenderer Renderer;

        private bool _visibilityDirty = true;

        protected virtual void Start() {
            Filter = gameObject.AddComponent<MeshFilter>();
            Renderer = gameObject.AddComponent<MeshRenderer>();
            transform.position += new Vector3(0.5f, 0, 0.5f);
        }

        protected virtual void Update() {
            if (_visibilityDirty) {
                RefreshView();
                _visibilityDirty = false;
            }
        }

        public void MarkVisibilityDirty() {
            _visibilityDirty = true;
        }

        public abstract bool IsFaceOpaque(Direction direction);

        public abstract bool Collides(Direction fromFace, Vector3 current, Vector3 origin, Vector3 direction,
            out Direction face, out Vector3 collision);

        protected void RefreshView() {
            var position = Block.Position;
            var count = 0;
            DirectionUtils.ForEach(direction => {
                var pos = position.Moved(direction);
                var block = pos.Block;
                if (block != null && block.View.IsFaceOpaque(direction.GetOpposite())) count++;
            });
            Renderer.enabled = count < 6;
            if (Renderer.enabled) {
                Filter.mesh = LoadMesh();
                Renderer.material = LoadMaterial();
            }
        }

        protected abstract Mesh LoadMesh();
        protected abstract Material LoadMaterial();
    }
}