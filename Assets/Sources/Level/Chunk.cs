using Sources.Identification;
using Sources.Level.Data;
using Sources.Registration;
using Sources.Util;
using UnityEngine;

namespace Sources.Level {
    public class Chunk {
        public const int ChunkLength = 16;
        public const int ChunkSize = ChunkLength * ChunkLength;
        public const int ChunkVolume = ChunkLength * ChunkLength * ChunkLength;
        public const int WorldToChunkPositionShift = 4;

        public Vector3Int Position { get; }
        private readonly Block[,,] _blocks = new Block[ChunkLength, ChunkLength, ChunkLength];

        public Block GetBlock(Vector3Int position) {
            position.isInRange(0, ChunkLength - 1).ValidateTrue($"Position out of range! {position}");
            return _blocks[position.x, position.y, position.z];
        }

        public Block PlaceBlock(BlockData data, Vector3Int position) {
            position.isInRange(0, ChunkLength - 1).ValidateTrue($"Position out of range! {position}");

            var builder = Registry.Get<Block.Builder>(Identifiers.ManagerBlock)[data.Identifier];
            if (builder == null) return null;
            var block = builder.CreateBlock(new ChunkPosition(Position, position).toWorldPosition(), data);

            _blocks[position.x, position.y, position.z]?.Invalidate();
            _blocks[position.x, position.y, position.z] = block;
            return block;
        }
    }
}