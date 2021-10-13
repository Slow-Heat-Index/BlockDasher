using System.Collections;
using System.Collections.Generic;
using Level.Generator;
using Sources.Level;
using UnityEngine;
using DG.Tweening;

namespace Player.Data {
    public class PlayerData : MonoBehaviour {
        public LevelGenerator level;
        public int blocksPerDash = 2;
        public float movementSpeed;
        
        private Queue<Vector3> _movementStack = new Queue<Vector3>();
        private Tween _currentMovement;

        public BlockPosition BlockPosition { get; private set; }

        private void Awake() {
            BlockPosition = level.World.StartPosition.Position;
            UpdateTransform();
        }

        public void Move(Vector3Int offset) {
            BlockPosition = BlockPosition.Moved(offset);
            
            
            if (_currentMovement != null && _currentMovement.IsActive() && !_currentMovement.IsComplete()) {

                _movementStack.Enqueue(offset);
            }
            else {
                _currentMovement = transform.DOMove(BlockPosition.Position + new Vector3(0.5f, 0.5f, 0.5f), movementSpeed)
                    .OnComplete(CheckStack);
            }
            
            //UpdateTransform();
        }

        private void CheckStack() {
            if (_movementStack.Count == 0) return;

            var offset = _movementStack.Dequeue();
            _currentMovement = transform.DOMove(transform.position + offset, movementSpeed)
                .OnComplete(CheckStack);
        }

        public void Teleport(Vector3Int position) {
            BlockPosition = new BlockPosition(BlockPosition.World, position);
            UpdateTransform();
        }


        private void UpdateTransform() {
            transform.position = BlockPosition.Position + new Vector3(0.5f, 0.5f, 0.5f);
        }
    }
}