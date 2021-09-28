using Sources.Level;
using UnityEngine;

namespace Level {
    public class BlockView : MonoBehaviour {
        public Block block;

        private void Start() {
            Debug.Log($"START {block?.Identifier}");
        }
    }
}