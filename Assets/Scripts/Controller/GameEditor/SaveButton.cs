using System.IO;
using Sources;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.GameEditor {
    [RequireComponent(typeof(Button))]
    public class SaveButton : MonoBehaviour {
        private void Start() {
            GetComponent<Button>().onClick.AddListener(() => {
#if UNITY_EDITOR
                string path = EditorUtility.SaveFilePanel("Save world", "", "world", "bytes");
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

            print("Loading file " + file + "...");
            using var binary = new BinaryWriter(File.OpenWrite(file));
            FindObjectOfType<EditorData>().World.Write(binary);
            print("Done.");
        }
    }
}