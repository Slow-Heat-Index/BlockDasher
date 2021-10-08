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
        private static readonly Vector2Int[] _2DVectors = {
            Vector2Int.zero, Vector2Int.zero, Vector2Int.up, Vector2Int.down, Vector2Int.right, Vector2Int.left
        };

        private static readonly Vector3Int[] _vectors = {
            Vector3Int.up, Vector3Int.down, Vector3Int.forward, Vector3Int.back, Vector3Int.right, Vector3Int.left
        };

        private static readonly Direction[] _opposite = {
            Direction.Down, Direction.Up, Direction.South, Direction.North, Direction.West, Direction.East
        };

        private static readonly Direction[,] _rotations = {
            { Direction.North, Direction.South, Direction.East, Direction.West },
            { Direction.South, Direction.North, Direction.West, Direction.East },
            { Direction.East, Direction.West, Direction.South, Direction.North },
            { Direction.West, Direction.East, Direction.North, Direction.South },
        };

        public static Vector3Int GetVector(this Direction direction) => _vectors[(int)direction];

        public static Vector2Int Get2DVector(this Direction direction) => _2DVectors[(int)direction];

        public static Direction GetOpposite(this Direction direction) => _opposite[(int)direction];

        /**
         * Rotates this direction, making the given rotation the north value.
         *
         * For example: if this direction is West and the rotation is East, this method returns South.
         * 
         *                               rotation (Now North)
         *          |                            |
         *          |                            |
         *  this ---|--- rotation  --->    ------|------
         *          |                            |
         *          |                            |
         *                              returned value (South)
         * 
         */ 
        public static Direction Rotated(this Direction direction, Direction rotation) {
            if (direction == Direction.Up || direction == Direction.Down) return direction;
            if (rotation == Direction.Up || rotation == Direction.Down) return direction;
            return _rotations[(int)rotation - 2, (int)direction - 2];
        }

        public static void ForEach(Action<Direction> action) {
            for (var i = 0; i < 6; i++) {
                action((Direction)i);
            }
        }
    }
}