using Sources.Level;
using Sources.Util;
using UnityEngine;

namespace Level {
    public abstract class BlockView : MonoBehaviour {
        public Block Block;

        public bool staticBlock;
        private bool _visibilityDirty = true;

        public MeshFilter Filter { get; private set; }
        public MeshRenderer Renderer { get; private set; }

        public Material Material { get; private set; }

        protected virtual void Update() {
            RefreshViewIfRequired();
        }

        public virtual void Initialize() {
            staticBlock = FindObjectOfType<EditorData>() == null;
            Filter = gameObject.AddComponent<MeshFilter>();
            Filter.mesh = LoadMesh();
            Material = LoadMaterial();

            if (!staticBlock) {
                Renderer = gameObject.AddComponent<MeshRenderer>();
                Renderer.material = Material;
            }

            transform.position += new Vector3(0.5f, 0, 0.5f);
        }


        public void MarkVisibilityDirty() {
            _visibilityDirty = true;
        }

        public abstract bool IsFaceOpaque(Direction direction);

        public abstract bool Collides(Direction fromFace, Vector3 current, Vector3 origin, Vector3 direction,
            out Direction face, out Vector3 collision);

        public void RefreshViewIfRequired() {
            if (_visibilityDirty) {
                try {
                    RefreshView();
                }
                finally {
                    _visibilityDirty = false;
                }
            }
        }

        protected void RefreshView() {
            var position = Block.Position;
            var count = 0;
            DirectionUtils.ForEach(direction => {
                var pos = position.Moved(direction);
                var block = pos.Block;
                if (block != null && block.View.IsFaceOpaque(direction.GetOpposite())) count++;
            });
            Material = LoadMaterial();
            if (staticBlock) return;
            Renderer.enabled = count < 6;
            if (Renderer.enabled) {
                Renderer.material = LoadMaterial();
            }
        }

        protected abstract Mesh LoadMesh();
        protected abstract Material LoadMaterial();
    }
}