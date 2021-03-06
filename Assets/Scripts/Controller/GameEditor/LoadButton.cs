using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.GameEditor {
    [RequireComponent(typeof(Button))]
    public class LoadButton : MonoBehaviour {
        private void Start() {
            GetComponent<Button>().onClick.AddListener(() => {
#if UNITY_EDITOR
                string path = EditorUtility.OpenFilePanel("Load world", "", "bytes");
                LoadFile(path);
#else
                print("Stud!");
#endif
            });
        }


        private void LoadFile(string file) {
            if (file.Length == 0) {
                print("File is empty!");
                return;
            }

            print("Saving file " + file + "...");
            using var binary = new BinaryReader(File.OpenRead(file));
            FindObjectOfType<EditorData>().World.Read(binary);
            print("Done.");
        }
    }
}