using System;
using UnityEngine;

namespace Sources.Util {
    public enum Direction {
        Up,
        Down,
        North,
        South,
        East,
        West
    }

    public static class DirectionUtils {
        private static readonly Vector3Int[] _vectors = {
            Vector3Int.up, Vector3Int.down, Vector3Int.forward, Vector3Int.back, Vector3Int.right, Vector3Int.left
        };

        private static readonly Direction[] _opposite = {
            Direction.Down, Direction.Up, Direction.South, Direction.North, Direction.West, Direction.East
        };

        public static Vector3Int GetVector(this Direction direction) => _vectors[(int)direction];

        public static Direction GetOpposite(this Direction direction) => _opposite[(int)direction];

        public static void ForEach(Action<Direction> action) {
            for (var i = 0; i < 6; i++) {
                action((Direction)i);
            }
        }
    }
}