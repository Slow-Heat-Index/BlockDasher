using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Level.Entities;
using Sources.Identification;
using Sources.Level.Blocks;
using Sources.Level.Data;
using Sources.Level.Manager;
using Sources.Level.Skyboxes;
using Sources.Registration;
using Sources.Util;
using UnityEngine;
using Object = UnityEngine.Object;

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

        public int GoldMoves { get; set; }
        public int SilverMoves { get; set; }

        public SkyboxWrapper Skybox { get; set; } = SkyboxManager.Garden;

        public bool IsEditorWorld { get; }

        private readonly Dictionary<Vector3Int, Chunk> _chunks = new Dictionary<Vector3Int, Chunk>();
        private readonly List<Entity> _entities = new List<Entity>();

        public World(bool editorWorld) {
            IsEditorWorld = editorWorld;
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

        public void ResetLevel(bool spawnEntities) {
            _entities.ForEach(it => Object.Destroy(it.gameObject));
            _entities.Clear();
            foreach (var chunk in _chunks.Values) {
                chunk.ResetChunk(spawnEntities);
            }
        }

        public void SpawnEntities() {
            foreach (var chunk in _chunks.Values) {
                chunk.SpawnEntities();
            }
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
            writer.Write(2u);

            writer.Write(GoldMoves);
            writer.Write(SilverMoves);
            writer.Write(Skybox.Identifier);

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
            if (version == 0) {
                /*InitialMoves =*/
                reader.ReadUInt32();
                GoldMoves = 10;
                SilverMoves = 20;
            }

            if (version >= 1) {
                GoldMoves = reader.ReadInt32();
                SilverMoves = reader.ReadInt32();
                if (version == 1) {
                    reader.ReadInt32();
                }
            }

            if (version >= 2) {
                Skybox = Registry.Get<SkyboxWrapper>(Identifiers.ManagerSkybox)
                    .Get(reader.ReadIdentifier()) ?? SkyboxManager.Garden;
            }
            else {
                Skybox = SkyboxManager.Garden;
            }

            foreach (var chunk in _chunks.Values) {
                chunk.Clear();
            }

            var amount = reader.ReadInt32();
            for (var i = 0; i < amount; i++) {
                var position = reader.ReadVector3Int();
                var chunk = GetOrCreateChunk(position);
                chunk.Read(reader);
            }
            
            RenderSettings.skybox = Skybox.Skybox;
        }
    }
}