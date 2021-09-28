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

        public readonly World World;
        public readonly Vector3Int Position;

        private readonly Block[,,] _blocks = new Block[ChunkLength, ChunkLength, ChunkLength];

        public Chunk(World world, Vector3Int position) {
            World = world;
            Position = position;
        }

        public Block GetBlock(Vector3Int position) {
            position.isInRange(0, ChunkLength - 1).ValidateTrue($"Position out of range! {position}");
            return _blocks[position.y, position.x, position.z];
        }

        public Block PlaceBlock(BlockData data, Vector3Int position, bool refreshAdjacent = true) {
            position.isInRange(0, ChunkLength - 1).ValidateTrue($"Position out of range! {position}");

            var worldPosition = new ChunkPosition(Position, position).toWorldPosition();
            var blockPosition = new BlockPosition(World, worldPosition);
            Block block = null;
            
            if (data.Identifier == null) {
                RemoveBlock(position);
            }
            else {
                var builder = Registry.Get<BlockType>(Identifiers.ManagerBlock)[data.Identifier];
                if (builder == null) {
                    RemoveBlock(position);
                }
                else {
                    block = builder.CreateBlock(blockPosition, data);
                    _blocks[position.y, position.x, position.z]?.Invalidate();
                    _blocks[position.y, position.x, position.z] = block;
                }
            }

            if (refreshAdjacent) {
                blockPosition.ForEachAdjacentBlock(it => it.View.MarkVisibilityDirty());
            }
            
            return block;
        }

        public void RemoveBlock(Vector3Int position) {
            _blocks[position.y, position.x, position.z]?.Invalidate();
            _blocks[position.y, position.x, position.z] = null;
        }

        public void Clear() {
            for (var y = 0; y < ChunkLength; y++) {
                for (var x = 0; x < ChunkLength; x++) {
                    for (var z = 0; z < ChunkLength; z++) {
                        _blocks[y, x, z]?.Invalidate();
                        _blocks[y, x, z] = null;
                    }
                }
            }
        }
    }
}