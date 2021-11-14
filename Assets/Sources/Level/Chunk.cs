using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sources.Identification;
using Sources.Level.Blocks;
using Sources.Level.Data;
using Sources.Registration;
using Sources.Util;
using UnityEngine;

namespace Sources.Level {
    /**
     * Represents a 16x16x16 set of blocks.
     *
     * Yeah, it just works like Minecraft...
     */
    public class Chunk {
        public const int ChunkLength = 16;
        public const int ChunkSize = ChunkLength * ChunkLength;
        public const int ChunkVolume = ChunkLength * ChunkLength * ChunkLength;
        public const int WorldToChunkPositionShift = 4;

        public readonly World World;
        public readonly Vector3Int Position;

        private readonly Block[] _blocks = new Block[ChunkLength * ChunkLength * ChunkLength];

        private readonly Dictionary<Vector3Int, SpawnerBlock> _spawners = new Dictionary<Vector3Int, SpawnerBlock>();

        private readonly Dictionary<Vector3Int, BlockData> _modifiedInitialStates =
            new Dictionary<Vector3Int, BlockData>();

        private int _blockCount = 0;

        public Chunk(World world, Vector3Int position) {
            World = world;
            Position = position;
        }

        /**
         * Returns the block located at the given position.
         * 
         * Tip! Use ChunkPosition if you want to transform a BlockPosition
         * or a world position into a local chunk position.
         * 
         * <param name="position">The local chunk position. (From (0,0,0) to (15,15, 15))</param>
         * 
         * <returns>The block located at the given position.</returns>
         */
        public Block GetBlock(Vector3Int position) {
            position.isInRange(0, ChunkLength - 1).ValidateTrue("Position out of range!");
            return Bl(position);
        }

        /**
         * Places a new Block into the given position.
         * 
         * If the identifier located at the given BlockData is null,
         * this method breaks a block instead (Places a null block!).
         *
         * Init should be true when this method is called from the editor ir when the world is being loaded.
         * If init is false, this chunk will record the initial state of the block. This is used to
         * reset the world fastly using ResetChunk.
         * 
         * <param name="data">The information of the block to create and place.</param>
         * <param name="position">The position of the new block.</param>
         * <param name="init">Whether this method is called from the editor of when the world is being loaded.</param>
         */
        public Block PlaceBlock(BlockData data, Vector3Int position, bool init = false) {
            position.isInRange(0, ChunkLength - 1).ValidateTrue($"Position out of range! {position}");

            var worldPosition = new ChunkPosition(Position, position).WorldPosition;
            var blockPosition = new BlockPosition(World, worldPosition);
            var old = Bl(position);
            Block block = null;

            if (data.Identifier == null) {
                if (old == null) return null;
                RemoveBlock(position, init);
                _blockCount--;
            }
            else {
                var builder = Registry.Get<BlockType>(Identifiers.ManagerBlock)[data.Identifier];
                if (builder == null) {
                    if (old == null) return null;
                    RemoveBlock(position, init);
                    _blockCount--;
                }
                else {
                    block = builder.CreateBlock(blockPosition, data);

                    if (!init) {
                        if (!_modifiedInitialStates.ContainsKey(position)) {
                            _modifiedInitialStates[position] = old?.ToBlockData() ?? new BlockData(null);
                        }
                    }

                    if (old != null) {
                        old.OnBreak();
                        old.Invalidate();
                    }
                    else {
                        _blockCount++;
                    }

                    _spawners.Remove(position);
                    if (block is SpawnerBlock spawner) {
                        _spawners[position] = spawner;
                    }

                    Bl(position, block);
                    block.OnPlace();
                }
            }

            blockPosition.ForEachAdjacentBlock(it => it.View.MarkVisibilityDirty());

            return block;
        }

        /**
         * <returns>Whether this Chunk has no Blocks.</returns>
         */
        public bool IsEmpty() {
            return _blockCount == 0;
        }

        /**
         * Breaks all Blocks inside this Chunk.
         * This also clears the initial states!
         */
        public void Clear() {
            _modifiedInitialStates.Clear();
            _spawners.Clear();
            for (var y = 0; y < ChunkLength; y++) {
                for (var x = 0; x < ChunkLength; x++) {
                    for (var z = 0; z < ChunkLength; z++) {
                        var b = Bl(x, y, z);
                        b?.OnBreak();
                        b?.Invalidate();
                        Bl(x, y, z, null);
                    }
                }
            }

            _blockCount = 0;
        }

        /**
         * Resets the Chunk to its initial state.
         */
        public void ResetChunk(bool spawnEntities) {
            foreach (var pair in _modifiedInitialStates) {
                PlaceBlock(pair.Value, pair.Key, true);
            }

            _modifiedInitialStates.Clear();

            if (!spawnEntities) return;
            foreach (var spawner in _spawners.Values) {
                spawner.Spawn();
            }
        }

        /**
         * Calls all spawners to spawn an entity.
         */
        public void SpawnEntities() {
            foreach (var spawner in _spawners.Values) {
                spawner.Spawn();
            }
        }

        /**
         * Saves this Chunk into the given BinaryWriter.
         *
         * <param name="writer">The writer.</param>
         */
        public void Write(BinaryWriter writer) {
            var set = new HashSet<Identifier>();

            for (var y = 0; y < ChunkLength; y++) {
                for (var x = 0; x < ChunkLength; x++) {
                    for (var z = 0; z < ChunkLength; z++) {
                        var block = Bl(x, y, z);
                        if (block == null) continue;
                        set.Add(block.Identifier);
                    }
                }
            }

            var identifiers = set.ToList();
            writer.Write(identifiers.Count);
            identifiers.ForEach(writer.Write);

            for (var y = 0; y < ChunkLength; y++) {
                for (var x = 0; x < ChunkLength; x++) {
                    for (var z = 0; z < ChunkLength; z++) {
                        writer.Write(Bl(x, y, z), identifiers);
                    }
                }
            }
        }

        /**
         * Loads the Chunk data stored into the given BinaryReader and places it into this Chunk.
         * <param name="reader">The reader.</param>
         */
        public void Read(BinaryReader reader) {
            var identifiersAmount = reader.ReadInt32();
            var identifiers = new List<Identifier>();
            for (var i = 0; i < identifiersAmount; i++) {
                identifiers.Add(reader.ReadIdentifier());
            }

            for (var y = 0; y < ChunkLength; y++) {
                for (var x = 0; x < ChunkLength; x++) {
                    for (var z = 0; z < ChunkLength; z++) {
                        PlaceBlock(reader.ReadBlockData(identifiers), new Vector3Int(x, y, z), true);
                    }
                }
            }
        }

        private void RemoveBlock(Vector3Int position, bool init) {
            var block = Bl(position);

            if (!init) {
                if (!_modifiedInitialStates.ContainsKey(position)) {
                    _modifiedInitialStates[position] = block?.ToBlockData() ?? new BlockData(null);
                }
            }

            if (block == null) return;
            block.OnBreak();
            block.Invalidate();

            _spawners.Remove(position);
            Bl(position, null);
        }

        private Block Bl(int x, int y, int z) {
            return _blocks[((y << WorldToChunkPositionShift) + x << WorldToChunkPositionShift) + z];
        }

        private Block Bl(Vector3Int pos) {
            return _blocks[((pos.y << WorldToChunkPositionShift) + pos.x << WorldToChunkPositionShift) + pos.z];
        }

        private void Bl(int x, int y, int z, Block block) {
            _blocks[((y << WorldToChunkPositionShift) + x << WorldToChunkPositionShift) + z] = block;
        }

        private void Bl(Vector3Int pos, Block block) {
            _blocks[((pos.y << WorldToChunkPositionShift) + pos.x << WorldToChunkPositionShift) + pos.z] = block;
        }
    }
}