using UnityEngine;

namespace Sources.Level {

    public class Chunk {
        public const int ChunkLength = 16;
        public const int ChunkSize = ChunkLength * ChunkLength;
        public const int ChunkVolume = ChunkLength * ChunkLength * ChunkLength;
        public const int WorldToChunkPositionShift = 4;

        private Vector3Int position;
    }
}