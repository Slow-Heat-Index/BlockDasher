using Sources.Level;
using UnityEngine;

namespace Controller.GameEditor {
    public class BlockTypeDisplay : MonoBehaviour {
        public MeshFilter meshFilter;
        public MeshRenderer meshRenderer;

        public BlockType BlockType;

        private void Start() {
            if (BlockType == null) {
                Debug.Log("DISPLAY IS NULL!");
                return;
            }

            meshFilter.mesh = BlockType.DefaultMesh;
            meshRenderer.material = BlockType.DefaultMaterial;
        }
    }
}