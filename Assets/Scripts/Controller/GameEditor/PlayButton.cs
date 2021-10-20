using UnityEngine;
using UnityEngine.UI;

namespace Controller.GameEditor {
    [RequireComponent(typeof(Button))]
    public class PlayButton : MonoBehaviour {
        private void Start() {
            GetComponent<Button>().onClick.AddListener(FindObjectOfType<EditorData>().PlayEditorLevel);
        }
    }
}