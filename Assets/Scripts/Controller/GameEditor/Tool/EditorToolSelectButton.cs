using UnityEngine;
using UnityEngine.UI;

namespace Controller.GameEditor.Tool {
    [RequireComponent(typeof(Button), typeof(Image))]
    public class EditorToolSelectButton : MonoBehaviour {
        public EditorToolType type;
        private Image _image;

        private EditorData _editorData;

        private void Start() {
            _editorData = FindObjectOfType<EditorData>();
            _image = GetComponent<Image>();
            GetComponent<Button>().onClick.AddListener(() => _editorData.selectedEditorTool = type);
        }

        private void Update() {
            _image.color = _editorData.selectedEditorTool == type ? Color.red : Color.white;
        }
    }
}