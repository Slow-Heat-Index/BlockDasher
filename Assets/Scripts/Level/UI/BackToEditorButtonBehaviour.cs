using UnityEngine;

namespace Level.UI {
    public class BackToEditorButtonBehaviour : MonoBehaviour {
        private void Start() {
            var editorData = FindObjectOfType<EditorData>();
            if (editorData != null && editorData.editorPlaying) return;
            Destroy(gameObject);
        }

        public void OnClick() {
            FindObjectOfType<EditorData>().StopPlayingEditorLevel();
        }
    }
}