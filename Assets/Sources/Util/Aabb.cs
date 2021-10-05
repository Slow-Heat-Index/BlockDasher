using System;
using System.Linq;
using UnityEngine;

namespace Sources.Util {
    /// <summary>
    /// Represents an axis-aligned bounding box.
    /// </summary>
    public class Aabb {
        private readonly float _x, _y, _z, _width, _height, _depth;

        /// <summary>
        /// Creates an axis-aligned bounding box using the local position and the size of the box.
        /// </summary>
        /// <param name="x">The x local position parameter.</param>
        /// <param name="y">The y local position parameter.</param>
        /// <param name="z">The z local position parameter.</param>
        /// <param name="width">The width of the box.</param>
        /// <param name="height">The height of the box.</param>
        /// <param name="depth">The depth of the box.</param>
        public Aabb(float x, float y, float z, float width, float height, float depth) {
            _x = x;
            _y = y;
            _z = z;
            _width = width;
            _height = height;
            _depth = depth;
        }

        /// <summary>
        /// THe x local position parameter.
        /// </summary>
        public float X => _x;

        /// <summary>
        /// THe y local position parameter.
        /// </summary>
        public float Y => _y;

        /// <summary>
        /// THe z local position parameter.
        /// </summary>
        public float Z => _z;

        /// <summary>
        /// THe width of the box.
        /// </summary>
        public float Width => _width;

        /// <summary>
        /// THe height the box.
        /// </summary>
        public float Height => _height;

        /// <summary>
        /// THe depth of the box.
        /// </summary>
        public float Depth => _depth;

        /// <summary>
        /// The minimum local position of the box.
        /// </summary>
        public Vector3 Min => new Vector3(_x, _y, _z);

        /// <summary>
        /// The maximum local position of the box.
        /// </summary>
        public Vector3 Max => new Vector3(_x + _width, _y + _height, _z + _depth);

        public Vector3[] GetVertices() {
            var vertices = new Vector3[8];
            for (var ix = 0; ix < 2; ix++) {
                for (var iy = 0; iy < 2; iy++) {
                    for (var iz = 0; iz < 2; iz++) {
                        vertices[(ix << 2) + (iy << 1) + iz] =
                            new Vector3(_x + _width * ix, _y + _height * iy, _z + _depth * iz);
                    }
                }
            }

            return vertices;
        }

        /// <summary>
        /// Checks whether a point is inside of the box.
        /// Being at the edge of the box is considered to be inside.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="thisPosition">The world position of the box</param>
        /// <returns>Whether the point is inside.</returns>
        public bool Collides(Vector3 point, Vector3 thisPosition) {
            var tx = _x + thisPosition.x;
            var ty = _y + thisPosition.y;
            var tz = _z + thisPosition.z;

            //Using separating axis theorem:
            return tx <= point.x && tx + _width >= point.x &&
                   ty <= point.y && ty + _height >= point.y &&
                   tz <= point.z && tz + _depth >= point.z;
        }


        /// <summary>
        /// Checks whether two axis-aligned bounding boxes collides.
        /// The algorithm used is a simplified implementation of the separating axis theorem: http://www.dyn4j.org/2010/01/sat/.
        /// If a collision is detected a CollisionData instance holding all the collision info is stored in the parameter "data".
        /// </summary>
        /// <param name="o">The other AABB.</param>
        /// <param name="thisPosition">The world position of this AABB.</param>
        /// <param name="otherPosition">The world position of the other AABB.</param>
        /// <param name="faces">The faces that can collides. This is used to avoid collision errors between cubic blocks.</param>
        /// <param name="data">The collision data, or null if there is no collision.</param>
        /// <returns>Whether the two boxes collides.</returns>
        public bool Collides(Aabb o, Vector3 thisPosition, Vector3 otherPosition, bool[] faces,
            out CollisionData data) {
            if (faces != null && !faces.Any(f => f)) {
                data = null;
                return false;
            }

            var tx = _x + thisPosition.x;
            var ty = _y + thisPosition.y;
            var tz = _z + thisPosition.z;
            var ox = o._x + otherPosition.x;
            var oy = o._y + otherPosition.y;
            var oz = o._z + otherPosition.z;

            var dxa = tx + _width - ox;
            var dxb = ox + o._width - tx;

            var dya = ty + _height - oy;
            var dyb = oy + o._height - ty;

            var dza = tz + _depth - oz;
            var dzb = oz + o._depth - tz;

            if (dxa <= 0 || dxb <= 0 || dya <= 0 || dyb <= 0 || dza <= 0 || dzb <= 0) {
                data = null;
                return false;
            }

            //Using the separating axis theorem: if all axis checks are greater than 0, then the minimum distance
            //to resolve the collision is the minimum axis check.

            var distance = dxa;
            var face = Direction.West;

            if (dxb < distance) {
                distance = dxb;
                face = Direction.East;
            }

            if (dya < distance) {
                distance = dya;
                face = Direction.Down;
            }

            if (dyb < distance) {
                distance = dyb;
                face = Direction.Up;
            }

            if (dza < distance) {
                distance = dza;
                face = Direction.North;
            }

            if (dzb < distance) {
                distance = dzb;
                face = Direction.South;
            }

            data = new CollisionData(face, distance);
            return faces == null || faces[(int)face];
        }

        /// <summary>
        /// Returns whether a segment collides with the box.
        /// This is a simplified implementation of this algorithm: https://www.youtube.com/watch?v=USjbg5QXk3g.
        /// </summary>
        /// <param name="boxPosition">The world position of this box.</param>
        /// <param name="min">The minimum segment position.</param>
        /// <param name="max">The maximum segment position.</param>
        /// <param name="collision">The collision point. (Starting from min)</param>
        /// <param name="face">The collision face.</param>
        /// <returns>Whether the segment collides the box.</returns>
        public bool CollidesSegment(Vector3 boxPosition, Vector3 min, Vector3 max, out Vector3 collision,
            out Direction face) {
            float low = 0;
            float high = 1;

            var boxMin = Min + boxPosition;
            var boxMax = Max + boxPosition;

            face = Direction.Up;
            //Test all three dimensions.
            for (var i = 0; i < 3; i++) {
                if (ClipLine(i, ref boxMin, ref boxMax, ref min, ref max, ref low, ref high, out var higher)) {
                    if (higher) {
                        var positive = max[i] - min[i] >= 0;
                        face = i switch {
                            0 => (positive ? Direction.West : Direction.East),
                            1 => (positive ? Direction.Down : Direction.Up),
                            2 => (positive ? Direction.North : Direction.South),
                            _ => Direction.Up
                        };
                    }

                    continue;
                }

                collision = max;
                return false;
            }

            collision = min + low * (max - min);

            return true;
        }

        private bool ClipLine(int dimension, ref Vector3 boxMin, ref Vector3 boxMax, ref Vector3 min,
            ref Vector3 max,
            ref float low, ref float high, out bool higher) {
            var maxMin = max[dimension] - min[dimension];
            var dimLow = (boxMin[dimension] - min[dimension]) / maxMin;
            var dimHigh = (boxMax[dimension] - min[dimension]) / maxMin;

            if (dimLow > dimHigh) {
                (dimLow, dimHigh) = (dimHigh, dimLow);
            }

            higher = dimLow >= low;
            if (dimHigh < low) return false;
            if (dimLow > high) return false;
            low = Math.Max(dimLow, low);
            high = Math.Min(dimHigh, high);

            return low <= high;
        }
    }

    /// <summary>
    /// Represents the result of a collision test if a collision was detected.
    /// </summary>
    public class CollisionData {
        private Direction _blockFace;
        private float _distance;

        /// <summary>
        /// Creates a collision data using a penetrated block face and the penetration distance.
        /// </summary>
        /// <param name="blockFace">the block face</param>
        /// <param name="distance">the distance</param>
        public CollisionData(Direction blockFace, float distance) {
            _blockFace = blockFace;
            _distance = distance;
        }

        /// <summary>
        /// The penetrated block face.
        /// </summary>
        public Direction BlockFace => _blockFace;

        /// <summary>
        /// The penetration distance.
        /// </summary>
        public float Distance => _distance;
    }
}