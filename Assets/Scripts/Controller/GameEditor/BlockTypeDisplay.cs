using System.Collections.Generic;
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
        public bool showSelection = true;

        private BlockType _blockType;
        private EditorData _editorData;

        public BlockType BlockType {
            get => _blockType;
            set {
                _blockType = value;
                meshFilter.mesh = value.DefaultMesh;
                meshRenderer.material.SetTexture(ShaderTextureReference, value.DefaultTexture);
            }
        }

        private void Start() {
            _editorData = FindObjectOfType<EditorData>();
            if (BlockType == null) {
                Debug.Log("DISPLAY IS NULL!");
                return;
            }

            button = GetComponent<Button>();

            button.onClick.AddListener(() => {
                _editorData.SelectedBlockType = BlockType;
                _editorData.Metadata = new Dictionary<string, string>();
            });
        }

        private void Update() {
            meshRenderer.transform.rotation *= Quaternion.Euler(0, Time.deltaTime * 10, 0);
            button.image.color = showSelection && BlockType == _editorData.SelectedBlockType
                ? new Color(1, 0, 0, 0.5f)
                : new Color(0, 0, 0, 0);
        }
    }
}