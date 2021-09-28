using System;
using Sources.Level;
using UnityEngine;

namespace Sources.Util {
    public static class Vector3IUtils {
        public static bool isInRange(this Vector3Int v, int min, int max) =>
            v.x >= min && v.y >= min && v.z >= min && v.x <= max && v.y <= max && v.z <= max;

        public static ChunkPosition toChunkPosition(this Vector3Int v) => new ChunkPosition(v);

        public static Vector3Int Floor(this Vector3 v) =>
            new Vector3Int((int)Math.Floor(v.x), (int)Math.Floor(v.y), (int)Math.Floor(v.z));
    }
}