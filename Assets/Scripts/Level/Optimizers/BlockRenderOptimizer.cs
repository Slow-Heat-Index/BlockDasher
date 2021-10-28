using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace Level.Optimizers {
    public class BlockRenderOptimizer : MonoBehaviour {
        public Material material;

        private MeshFilter _filter;
        private MeshRenderer _renderer;

        private void Awake() {
            _filter = gameObject.AddComponent<MeshFilter>();
            _renderer = gameObject.AddComponent<MeshRenderer>();
        }

        public void Init(ICollection<BlockView> view) {
            if (view.Count == 0) return;
            var blocks = view.Where(it => it.Material == material).ToList();
            var i = 0;
            var combine = new CombineInstance[blocks.Count];
            foreach (var block in blocks) {
                combine[i].mesh = block.Filter.sharedMesh;
                combine[i].transform = block.transform.localToWorldMatrix;
                i++;
            }

            _filter.mesh = new Mesh();
            _filter.mesh.CombineMeshes(combine);
            _renderer.material = material;
            if (material != null) {
                gameObject.name = "Optimizer for " + material.name + " (" + blocks[0].Block.Identifier + ")";
            }
            gameObject.isStatic = true;
        }
    }
}