using Level.Generator;
using Sources.Level;
using UnityEngine;

namespace Player.Data {
    public class PlayerData : MonoBehaviour {
        public LevelGenerator level;
        public int blocksPerDash = 2;

        public BlockPosition BlockPosition { get; private set; }

        private void Start() {
            BlockPosition = level.World.StartPosition.Position;
        }

        private void Update() {
            transform.position = BlockPosition.Position + new Vector3(0.5f, 0.5f, 0.5f);
        }

        public void Move(Vector3Int offset) {
            BlockPosition = BlockPosition.Moved(offset);
        }
    }
}