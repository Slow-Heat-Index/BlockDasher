using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public Block PlaceBlock(BlockData data, Vector3Int position) {
            position.isInRange(0, ChunkLength - 1).ValidateTrue($"Position out of range! {position}");

            var worldPosition = new ChunkPosition(Position, position).toWorldPosition();
            var blockPosition = new BlockPosition(World, worldPosition);
            var old = _blocks[position.y, position.x, position.z];
            Block block = null;

            if (data.Identifier == null) {
                if (old == null) return null;
                RemoveBlock(position);
            }
            else {
                var builder = Registry.Get<BlockType>(Identifiers.ManagerBlock)[data.Identifier];
                if (builder == null) {
                    if (old == null) return null;
                    RemoveBlock(position);
                }
                else {
                    block = builder.CreateBlock(blockPosition, data);
                    old?.OnBreak();
                    old?.Invalidate();
                    _blocks[position.y, position.x, position.z] = block;
                    block.OnPlace();
                }
            }

            blockPosition.ForEachAdjacentBlock(it => it.View.MarkVisibilityDirty());

            return block;
        }

        public void RemoveBlock(Vector3Int position) {
            var block = _blocks[position.y, position.x, position.z];
            block?.OnBreak();
            block?.Invalidate();
            _blocks[position.y, position.x, position.z] = null;
        }

        public bool IsEmpty() {
            for (var y = 0; y < ChunkLength; y++) {
                for (var x = 0; x < ChunkLength; x++) {
                    for (var z = 0; z < ChunkLength; z++) {
                        if (_blocks[y, x, z] != null) return false;
                    }
                }
            }

            return true;
        }

        public void Clear() {
            for (var y = 0; y < ChunkLength; y++) {
                for (var x = 0; x < ChunkLength; x++) {
                    for (var z = 0; z < ChunkLength; z++) {
                        _blocks[y, x, z]?.OnBreak();
                        _blocks[y, x, z]?.Invalidate();
                        _blocks[y, x, z] = null;
                    }
                }
            }
        }


        public void Write(BinaryWriter writer) {
            var set = new HashSet<Identifier>();

            for (var y = 0; y < ChunkLength; y++) {
                for (var x = 0; x < ChunkLength; x++) {
                    for (var z = 0; z < ChunkLength; z++) {
                        var block = _blocks[y, x, z];
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
                        writer.Write(_blocks[y, x, z], identifiers);
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

            for (var y = 0; y < ChunkLength; y++) {
                for (var x = 0; x < ChunkLength; x++) {
                    for (var z = 0; z < ChunkLength; z++) {
                        PlaceBlock(reader.ReadBlockData(identifiers), new Vector3Int(x, y, z));
                    }
                }
            }
        }
    }
}