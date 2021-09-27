using UnityEngine;

namespace Sources.Util {
    public static class Vector3IUtils {
        public static bool isInRange(this Vector3Int v, int min, int max) =>
            v.x >= min && v.y >= min && v.z >= min && v.x <= max && v.y <= max && v.z <= max;
    }
}