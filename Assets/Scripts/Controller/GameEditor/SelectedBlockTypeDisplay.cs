using System;
using Sources;
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

        public BlockType BlockType {
            get => _blockType;
            set {
                _blockType = value;
                meshFilter.mesh = BlockType.DefaultMesh;
                meshRenderer.material.SetTexture(ShaderTextureReference, BlockType.DefaultTexture);
            }
        }

        private void Start() {
            BlockType = EditorData.SelectedBlockType;
            EditorData.OnSelectedBlockTypeChange += OnSelectedBlockChange;
        }


        private void Update() {
            meshRenderer.transform.rotation *= Quaternion.Euler(0, Time.deltaTime * 10, 0);
        }

        private void OnDestroy() {
            EditorData.OnSelectedBlockTypeChange -= OnSelectedBlockChange;
        }

        private void OnSelectedBlockChange(BlockType type) {
            BlockType = type;
        }
    }
}