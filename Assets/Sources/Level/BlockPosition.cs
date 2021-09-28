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
    }
}