using System;
using Sources.Util;
using UnityEngine;

namespace Sources.Level {
    /**
     * Represents a position inside a Chunk.
     *
     * This position is split into two positions: the chunk position and the local block position inside the chunk.
     * 
     */
    [Serializable]
    public struct ChunkPosition {
        [SerializeField] private Vector3Int chunk;
        [SerializeField] private Vector3Int position;

        public Vector3Int Chunk {
            get => chunk;
            set => chunk = value;
        }

        public Vector3Int Position {
            get => position;
            set {
                value.isInRange(0, Level.Chunk.ChunkLength - 1)
                    .ValidateTrue($"Position is not in range: {position}.");
                chunk = value;
            }
        }

        /**
         * The world position represented by this chunk position.
         */
        public Vector3Int WorldPosition => Chunk * 16 + position;

        public ChunkPosition(Vector3Int chunk, Vector3Int position) {
            position.isInRange(0, Level.Chunk.ChunkLength - 1)
                .ValidateTrue($"Position is not in range: {position}.");
            this.chunk = chunk;
            this.position = position;
        }

        public ChunkPosition(Vector3Int worldPosition) {
            chunk = new Vector3Int(
                worldPosition.x >> Level.Chunk.WorldToChunkPositionShift,
                worldPosition.y >> Level.Chunk.WorldToChunkPositionShift,
                worldPosition.z >> Level.Chunk.WorldToChunkPositionShift
            );

            var start = new Vector3Int(
                chunk.x << Level.Chunk.WorldToChunkPositionShift,
                chunk.y << Level.Chunk.WorldToChunkPositionShift,
                chunk.z << Level.Chunk.WorldToChunkPositionShift
            );

            position = worldPosition - start;
        }
    }
}