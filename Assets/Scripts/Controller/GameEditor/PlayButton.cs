using UnityEngine;
using UnityEngine.UI;

namespace Controller.GameEditor {
    [RequireComponent(typeof(Button))]
    public class PlayButton : MonoBehaviour {
        private void Start() {
            GetComponent<Button>().onClick.AddListener(() => {
                var data = FindObjectOfType<EditorData>();
                if (data.World.StartPosition == null) {
                    Debug.LogError("Cannot start play mode: start position not found!");
                    return;
                }
                data.PlayEditorLevel();
            });
        }
    }
}