using System.Collections.Generic;
using System.IO;
using Sources.Identification;
using Sources.Level;
using Sources.Level.Data;
using UnityEngine;

namespace Sources.Util {
    public static class BinaryWriterUtils {
        public static void Write(this BinaryWriter writer, Vector3Int vector) {
            writer.Write(vector.x);
            writer.Write(vector.y);
            writer.Write(vector.z);
        }

        public static void Write(this BinaryWriter writer, Identifier identifier) =>
            writer.Write(identifier.ToString());

        public static void Write(this BinaryWriter writer, ref BlockData block, List<Identifier> identifiers) {
            var id = block.Identifier == null ? -1 : identifiers.IndexOf(block.Identifier);
            if (id == -1) {
                writer.Write((byte)0);
            }
            else {
                writer.Write((byte)1);
                writer.Write(id);
                writer.Write(block.GetMetadataSize());
                block.ForEachMetadata((key, value) => {
                    writer.Write(key);
                    writer.Write(value);
                });
            }
        }
        
        public static void Write(this BinaryWriter writer, Block block, List<Identifier> identifiers) {
            var id = block == null ? -1 : identifiers.IndexOf(block.Identifier);
            if (id == -1) {
                writer.Write((byte)0);
            }
            else {
                writer.Write((byte)1);
                writer.Write(id);
                writer.Write(block.GetMetadataSize());
                block.ForEachMetadata((key, value) => {
                    writer.Write(key);
                    writer.Write(value);
                });
            }
        }
    }
}