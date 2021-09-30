using System;
using System.Collections.Generic;
using Sources.Identification;
using Sources.Util;
using UnityEngine;

namespace Sources.Level.Raycast {
    public class BlockRaycaster {
        private readonly World _world;

        private Vector3 _origin;
        private Vector3 _directionVector;
        private Vector3 _current;
        private float _maximumDistance, _maximumDistanceSquared;
        private Vector3Int _currentBlock;
        private Block _result;
        private Direction _face;
        private bool _finished;
        private List<Identifier> _bypassBlocks;

        public BlockRaycaster(World world, Vector3 origin, Vector3 direction, float maximumDistance) {
            _world = world;
            _origin = origin;
            _current = origin;
            _directionVector = direction.normalized;
            _maximumDistance = maximumDistance;
            _maximumDistanceSquared = maximumDistance * maximumDistance;
            _currentBlock = _current.Floor();
            _result = null;
            _finished = false;
            _face = Direction.Up;
            _bypassBlocks = new List<Identifier>();
        }

        public World World => _world;

        public Vector3 Origin => _origin;
        public Vector3 DirectionVector => _directionVector;

        public Vector3 Current => _current;

        public float MaximumDistance {
            get => _maximumDistance;
            set {
                _maximumDistance = value;
                _maximumDistanceSquared = _maximumDistance * _maximumDistance;
            }
        }

        public float MaximumDistanceSquared => _maximumDistanceSquared;

        public Block Result => _result;

        public Direction Face => _face;

        public List<Identifier> BypassBlocks {
            get => _bypassBlocks;
            set => _bypassBlocks = value;
        }

        public void Reset(Vector3 origin, Vector3 direction) {
            _origin = origin;
            _current = origin;
            _currentBlock = _current.Floor();
            _directionVector = direction.normalized;
            _result = null;
            _finished = false;
            _face = Direction.Up;
        }

        public void Run() {
            while (!_finished) {
                Step();
            }
        }

        public void Step() {
            var block = _world.GetBlock(_currentBlock);
            if (block != null && !_bypassBlocks.Contains(block.Identifier)) {
                if (block.View.Collides(_face, _current, _origin, _directionVector, out _face, out _current)) {
                    _result = block;
                    _finished = true;
                    return;
                }
            }

            var distanceX = GetDistance(_directionVector.x, _current.x, _currentBlock.x);
            var distanceY = GetDistance(_directionVector.y, _current.y, _currentBlock.y);
            var distanceZ = GetDistance(_directionVector.z, _current.z, _currentBlock.z);

            if (distanceX < distanceY) {
                if (distanceX < distanceZ) {
                    MoveToX(distanceX);
                }
                else {
                    MoveToZ(distanceZ);
                }
            }
            else {
                if (distanceY < distanceZ) {
                    MoveToY(distanceY);
                }
                else {
                    MoveToZ(distanceZ);
                }
            }

            if (_maximumDistanceSquared < (_current - _origin).sqrMagnitude) _finished = true;
        }

        private void MoveToX(float distance) {
            _current += _directionVector * distance;
            if (_directionVector.x < 0) {
                _face = Direction.East;
                _currentBlock.x--;
            }
            else {
                _currentBlock.x++;
                _face = Direction.West;
            }
        }

        private void MoveToY(float distance) {
            _current += _directionVector * distance;
            if (_directionVector.y < 0) {
                _currentBlock.y--;
                _face = Direction.Up;
            }
            else {
                _currentBlock.y++;
                _face = Direction.Down;
            }
        }

        private void MoveToZ(float distance) {
            _current += _directionVector * distance;
            if (_directionVector.z < 0) {
                _currentBlock.z--;
                _face = Direction.South;
            }
            else {
                _currentBlock.z++;
                _face = Direction.North;
            }
        }

        private static float GetDistance(float direction, float current, int currentBlock) {
            if (Math.Abs(direction) < 0.0001f) {
                return float.MaxValue;
            }

            var next = currentBlock + (direction > 0 ? 1 : 0);
            return (next - current) / direction;
        }
    }
}