using UnityEngine;

namespace Util {
    public static class VectorUtils {
        public static Vector3Int toInt(this Vector3 vec) {
            return new Vector3Int((int)vec.x, (int)vec.y, (int)vec.z);
        }

        public static Vector3 toFloat(this Vector3Int vec) {
            return new Vector3(vec.x, vec.y, vec.z);
        }
    }
}