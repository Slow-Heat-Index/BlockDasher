using System;
using Sources.Util;
using UnityEngine;

namespace Sources.Level {
    public struct BlockPosition {
        public World World;
        public Vector3Int Position;

        public Chunk Chunk => World.GetChunk(new ChunkPosition(Position).Chunk);
        public ChunkPosition ChunkPosition => new ChunkPosition(Position);
        public Block Block => World.GetBlock(Position);

        public BlockPosition(World world, Vector3Int position) {
            World = world;
            Position = position;
        }

        public BlockPosition Moved(Direction direction) {
            return new BlockPosition(World, Position + direction.GetVector());
        }

        public BlockPosition Moved(Vector3Int offset) {
            return new BlockPosition(World, Position + offset);
        }

        public void ForEachAdjacentBlock(Action<Block> action) {
            var position = this;
            DirectionUtils.ForEach(direction => {
                var relative = position.Moved(direction);
                var block = relative.Block;
                if (block != null) {
                    action(block);
                }
            });
        }

        public override string ToString() {
            return Position.ToString();
        }
    }
}