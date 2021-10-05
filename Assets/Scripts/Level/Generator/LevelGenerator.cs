using Sources;
using Sources.Level;
using UnityEngine;

namespace Level.Generator {
    public class LevelGenerator : MonoBehaviour {
        public readonly World World = new World(false);

        private void Start() {
            LevelData.LevelToLoad.Load(World);
        }
    }
}