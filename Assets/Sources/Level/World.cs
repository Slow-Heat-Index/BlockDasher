using System.Collections.Generic;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level {
    public class World {
        private readonly Dictionary<Vector3Int, Chunk> _chunks = new Dictionary<Vector3Int, Chunk>();

        public Chunk GetChunk(Vector3Int chunkPosition) {
            return _chunks[chunkPosition];
        }

        public Block GetBlock(Vector3Int position) {
            var chunkPosition = position.toChunkPosition();
            return _chunks.TryGetValue(chunkPosition.Chunk, out var chunk)
                ? chunk.GetBlock(chunkPosition.Position)
                : null;
        }

        public Block PlaceBlock(BlockData data, Vector3Int position) {
            var chunkPosition = position.toChunkPosition();
            return GetOrCreateChunk(chunkPosition.Chunk).PlaceBlock(data, chunkPosition.Position);
        }

        public void PlaceChunk(ChunkData data, Vector3Int chunkPosition) {
            if (_chunks.TryGetValue(chunkPosition, out var chunk)) {
                data.PlaceData(chunk);
            }
            else {
                _chunks[chunkPosition] = data.ToChunk(this, chunkPosition);
            }
        }

        private Chunk GetOrCreateChunk(Vector3Int chunkPosition) {
            if (_chunks.TryGetValue(chunkPosition, out var chunk)) return chunk;
            chunk = new Chunk(this, chunkPosition);
            _chunks[chunkPosition] = chunk;
            return chunk;
        }
    }
}