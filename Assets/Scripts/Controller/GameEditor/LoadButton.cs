using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.GameEditor {
    [RequireComponent(typeof(Button))]
    public class LoadButton : MonoBehaviour {
        private void Start() {
            GetComponent<Button>().onClick.AddListener(async () => {
                string path = EditorUtility.OpenFilePanel("Overwrite with png", "", "png");
                print(path);
            });
        }
    }
}