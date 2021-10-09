using System.Collections.Generic;
using Sources;
using Sources.Level;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.GameEditor {
    [RequireComponent(typeof(Button))]
    public class BlockTypeDisplay : MonoBehaviour {
        private static readonly int ShaderTextureReference = Shader.PropertyToID("BlockTexture");

        public Button button;
        public MeshFilter meshFilter;
        public MeshRenderer meshRenderer;

        public BlockType BlockType;

        private void Start() {
            if (BlockType == null) {
                Debug.Log("DISPLAY IS NULL!");
                return;
            }

            button = GetComponent<Button>();

            button.onClick.AddListener(() => {
                EditorData.SelectedBlockType = BlockType;
                EditorData.Metadata = new Dictionary<string, string>();
            });

            meshFilter.mesh = BlockType.DefaultMesh;
            meshRenderer.material.SetTexture(ShaderTextureReference, BlockType.DefaultTexture);
        }

        private void Update() {
            meshRenderer.transform.rotation *= Quaternion.Euler(0, Time.deltaTime * 10, 0);
            button.image.color = BlockType == EditorData.SelectedBlockType
                ? new Color(1, 0, 0, 0.5f)
                : new Color(0, 0, 0, 0);
        }
    }
}