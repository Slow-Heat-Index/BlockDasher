using System.Collections.Generic;
using System.IO;
using Sources.Identification;
using Sources.Level;
using Sources.Level.Data;
using UnityEngine;

namespace Sources.Util {
    public static class BinaryReaderUtils {
        public static Vector3Int ReadVector3Int(this BinaryReader reader) =>
            new Vector3Int(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());

        public static Identifier ReadIdentifier(this BinaryReader reader) =>
            new Identifier(reader.ReadString());

        public static BlockData ReadBlockData(this BinaryReader reader, List<Identifier> identifiers) {
            var found = reader.ReadByte();
            if (found == 0) return new BlockData();
            var index = reader.ReadInt32();
            if (index < 0 || index >= identifiers.Count) return new BlockData();
            
            var identifier = identifiers[index];
            var metadataSize = reader.ReadInt32();
            var metadata = new Dictionary<string, string>(metadataSize);

            for (var i = 0; i < metadataSize; i++) {
                metadata[reader.ReadString()] = reader.ReadString();
            }

            return new BlockData(identifier, metadata);
        }
    }
}