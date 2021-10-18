using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sources.Level.Blocks;
using Sources.Level.Data;
using Sources.Util;
using UnityEngine;

namespace Sources.Level {
    public class World {
        public StartBlock StartPosition { get; internal set; }

        public uint InitialMoves { get; set; }

        public bool IsEditorWorld { get; }

        private readonly Dictionary<Vector3Int, Chunk> _chunks = new Dictionary<Vector3Int, Chunk>();

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

        public Block PlaceBlock(BlockData data, Vector3Int position) {
            var chunkPosition = position.toChunkPosition();
            return GetOrCreateChunk(chunkPosition.Chunk).PlaceBlock(data, chunkPosition.Position);
        }

        public Chunk GetOrCreateChunk(Vector3Int chunkPosition) {
            if (_chunks.TryGetValue(chunkPosition, out var chunk)) return chunk;
            chunk = new Chunk(this, chunkPosition);
            _chunks[chunkPosition] = chunk;
            return chunk;
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