﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Level.Entities;
using Sources.Level.Blocks;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level {
    /**
     * Represents a World/Level.
     *
     * A World contains all the data about a level and its blocks.
     */
    public class World {
        /**
         * The start position of the world.
         * This is the block the Player will spawn.
         */
        public StartBlock StartPosition { get; internal set; }

        /**
         * The amount of moves the player can perform in this world.
         */
        public uint InitialMoves { get; set; }

        public bool IsEditorWorld { get; }

        private readonly Dictionary<Vector3Int, Chunk> _chunks = new Dictionary<Vector3Int, Chunk>();
        private readonly List<Entity> _entities = new List<Entity>();

        public World(bool editorWorld) {
            IsEditorWorld = editorWorld;
            InitialMoves = 20;
        }

        public Chunk GetChunk(Vector3Int chunkPosition) {
            return _chunks[chunkPosition];
        }

        public Block GetBlock(Vector3Int position) {
            var chunkPosition = position.toChunkPosition();
            return _chunks.TryGetValue(chunkPosition.Chunk, out var chunk)
                ? chunk.GetBlock(chunkPosition.Position)
                : null;
        }

        public Block PlaceBlock(BlockData data, Vector3Int position, bool init = false) {
            var chunkPosition = position.toChunkPosition();
            return GetOrCreateChunk(chunkPosition.Chunk).PlaceBlock(data, chunkPosition.Position, init);
        }

        public Chunk GetOrCreateChunk(Vector3Int chunkPosition) {
            if (_chunks.TryGetValue(chunkPosition, out var chunk)) return chunk;
            chunk = new Chunk(this, chunkPosition);
            _chunks[chunkPosition] = chunk;
            return chunk;
        }

        public void ResetLevel() {
            foreach (var chunk in _chunks.Values) {
                chunk.ResetChunk();
            }
            _entities.Clear();
        }

        public void AddEntity(Entity entity) {
            entity.ValidateNotNull("Entity cannot be null!");
            _entities.Add(entity);
        }

        public void RemoveEntity(Entity entity) {
            _entities.Remove(entity);
        }

        public void ForEachEntity(Action<Entity> action) {
            _entities.ForEach(action);
        }

        public void Write(BinaryWriter writer) {
            // Version
            writer.Write(0u);
            writer.Write(InitialMoves);

            var chunksToSave = _chunks.Where(pair => !pair.Value.IsEmpty())
                .ToDictionary(i => i.Key, i => i.Value);

            writer.Write(chunksToSave.Count);

            foreach (var pair in chunksToSave) {
                writer.Write(pair.Key);
                pair.Value.Write(writer);
            }
        }

        public void Read(BinaryReader reader) {
            var version = reader.ReadUInt32();
            InitialMoves = reader.ReadUInt32();

            foreach (var chunk in _chunks.Values) {
                chunk.Clear();
            }

            var amount = reader.ReadInt32();
            for (var i = 0; i < amount; i++) {
                var position = reader.ReadVector3Int();
                var chunk = GetOrCreateChunk(position);
                chunk.Read(reader);
            }
        }
    }
}