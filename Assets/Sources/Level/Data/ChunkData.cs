using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sources.Identification;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Data {
    public class ChunkData {
        public readonly BlockData[,,] Blocks;

        public ChunkData(Vector3Int position) {
            Blocks = new BlockData[Chunk.ChunkLength, Chunk.ChunkLength, Chunk.ChunkLength];
        }

        public void Write(BinaryWriter writer) {
            var set = new HashSet<Identifier>();

            for (var y = 0; y < Chunk.ChunkLength; y++) {
                for (var x = 0; x < Chunk.ChunkLength; x++) {
                    for (var z = 0; z < Chunk.ChunkLength; z++) {
                        var block = Blocks[y, x, z];
                        if (block.Identifier == null) continue;
                        set.Add(block.Identifier);
                    }
                }
            }

            var identifiers = set.ToList();
            writer.Write(identifiers.Count);
            identifiers.ForEach(writer.Write);

            for (var y = 0; y < Chunk.ChunkLength; y++) {
                for (var x = 0; x < Chunk.ChunkLength; x++) {
                    for (var z = 0; z < Chunk.ChunkLength; z++) {
                        writer.Write(ref Blocks[y, x, z], identifiers);
                    }
                }
            }
        }


        public void Read(BinaryReader reader) {
            var identifiersAmount = reader.ReadInt32();
            var identifiers = new List<Identifier>();
            for (var i = 0; i < identifiersAmount; i++) {
                identifiers.Add(reader.ReadIdentifier());
            }

            for (var y = 0; y < Chunk.ChunkLength; y++) {
                for (var x = 0; x < Chunk.ChunkLength; x++) {
                    for (var z = 0; z < Chunk.ChunkLength; z++) {
                        Blocks[y, x, z] = reader.ReadBlockData(identifiers);
                    }
                }
            }
        }

        public void PlaceData(Chunk chunk) {
            for (var y = 0; y < Chunk.ChunkLength; y++) {
                for (var x = 0; x < Chunk.ChunkLength; x++) {
                    for (var z = 0; z < Chunk.ChunkLength; z++) {
                        chunk.PlaceBlock(Blocks[y, x, z], new Vector3Int(x, y, z));
                    }
                }
            }
        }

        public Chunk ToChunk(World world, Vector3Int position) {
            var chunk = new Chunk(world, position);
            PlaceData(chunk);
            return chunk;
        }
    }
}