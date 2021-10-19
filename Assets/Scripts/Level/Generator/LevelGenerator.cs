using Sources;
using Sources.Level;
using UnityEngine;

namespace Level.Generator {
    public class LevelGenerator : MonoBehaviour {
        public readonly World World = new World(false);

        public GameObject player;

        private void Awake() {
            LevelData.LevelToLoad.Load(World);

            // Create player
            Instantiate(player);
        }
    }
}