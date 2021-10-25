using Sources.Level;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.GameEditor {
    [RequireComponent(typeof(Button))]
    public class SelectedBlockTypeDisplay : MonoBehaviour {
        private static readonly int ShaderTextureReference = Shader.PropertyToID("BlockTexture");

        public MeshFilter meshFilter;
        public MeshRenderer meshRenderer;

        private BlockType _blockType;
        private EditorData _editorData;

        public BlockType BlockType {
            get => _blockType;
            set {
                _blockType = value;
                meshFilter.mesh = BlockType.DefaultMesh;
                meshRenderer.material.SetTexture(ShaderTextureReference, BlockType.DefaultTexture);
                _blockType.EditEditorDisplay(gameObject, meshFilter, meshRenderer);
            }
        }

        private void Start() {
            _editorData = FindObjectOfType<EditorData>();
            BlockType = _editorData.SelectedBlockType;
            _editorData.OnSelectedBlockTypeChange += OnSelectedBlockChange;
        }


        private void Update() {
            meshRenderer.transform.rotation *= Quaternion.Euler(0, Time.deltaTime * 10, 0);
        }

        private void OnDestroy() {
            _editorData.OnSelectedBlockTypeChange -= OnSelectedBlockChange;
        }

        private void OnSelectedBlockChange(BlockType type) {
            BlockType = type;
        }
    }
}