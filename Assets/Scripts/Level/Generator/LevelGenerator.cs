using Sources;
using Sources.Level;
using UnityEngine;

namespace Level.Generator {
    public class LevelGenerator : MonoBehaviour {
        public readonly World World = new World(false);

        private void Awake() {
            LevelData.LevelToLoad.Load(World);
        }
    }
}