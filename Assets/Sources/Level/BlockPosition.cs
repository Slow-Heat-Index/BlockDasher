using System;
using Sources.Util;
using UnityEngine;

namespace Sources.Level {
    /**
     * Represents a position inside a world.
     * Used to represent a block's position.
     *
     * This struct contains the World and a Vector3Int representing the raw position.
     */
    public struct BlockPosition {
        /**
         * The World.
         */
        public World World;

        /**
         * The raw BlockPosition.
         */
        public Vector3Int Position;

        /**
         * The chunk where this BlockPosition is located at.
         * It may be null!
         */
        public Chunk Chunk => World.GetChunk(new ChunkPosition(Position).Chunk);

        /**
         * This BlockPosition represented as a ChunkPosition.
         */
        public ChunkPosition ChunkPosition => new ChunkPosition(Position);

        /**
         * The Block located at this position.
         * It may be null!
         */
        public Block Block => World.GetBlock(Position);

        /**
         * Creates a BlockPosition.
         * <param name="world">The World.</param>
         * <param name="position">The position.</param>
         */
        public BlockPosition(World world, Vector3Int position) {
            world.ValidateNotNull("World cannot be null!");
            world.ValidateNotNull("Position cannot be null!");
            World = world;
            Position = position;
        }

        /**
         * <param name="direction">The direction.</param>
         * <returns>A copy of this BlockPosition moved to the given direction.</returns>
         */
        public BlockPosition Moved(Direction direction) {
            return new BlockPosition(World, Position + direction.GetVector());
        }

        /**
         * <param name="offset">The positions to move.</param>
         *  <returns>A copy of this BlockPosition moved the given offset.</returns>
         */
        public BlockPosition Moved(Vector3Int offset) {
            return new BlockPosition(World, Position + offset);
        }

        /**
         * Executes the given action for each adjacent block.
         * This doesn't include null blocks!
         *
         * <param name="action">The action to execute.</param>
         */
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