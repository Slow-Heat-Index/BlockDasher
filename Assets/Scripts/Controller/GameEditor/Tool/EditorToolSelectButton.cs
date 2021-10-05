using Sources;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.GameEditor.Tool {
    [RequireComponent(typeof(Button), typeof(Image))]
    public class EditorToolSelectButton : MonoBehaviour {
        public EditorToolType type;
        private Image _image;

        private void Start() {
            _image = GetComponent<Image>();
            GetComponent<Button>().onClick.AddListener(() => EditorData.SelectedEditorTool = type);
        }

        private void Update() {
            _image.color = EditorData.SelectedEditorTool == type ? Color.red : Color.white;
        }
    }
}