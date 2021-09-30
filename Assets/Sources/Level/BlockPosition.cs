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

        public void Move(Direction direction) {
            Position += direction.GetVector();
        }

        public void ForEachAdjacentBlock(Action<Block> action) {
            var position = this;
            DirectionUtils.ForEach(direction => {
                var relative = position;
                relative.Move(direction);
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