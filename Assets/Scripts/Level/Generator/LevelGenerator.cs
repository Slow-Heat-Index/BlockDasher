using Sources;
using Sources.Level;
using UnityEngine;

namespace Level.Generator {
    public class LevelGenerator : MonoBehaviour {
        public GameObject player;
        public World World { get; private set; }

        private void Awake() {
            var editorData = FindObjectOfType<EditorData>();

            if (editorData == null || !editorData.editorPlaying) {
                World = new World(false);
                LevelData.LevelToLoad.Load(World);
            }
            else {
                World = editorData.World;
            }

            // Create player
            Instantiate(player, transform);
        }
    }
}