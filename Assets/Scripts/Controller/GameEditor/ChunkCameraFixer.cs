using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller.GameEditor {
    [RequireComponent(typeof(PlayerInput))]
    public class ChunkCameraFixer : MonoBehaviour {
        private Vector3Int _chunk = new Vector3Int(0, 0, 0);
        private float _radius = 20.0f;

        private Vector2 _rotation;

        private InputAction _cameraRotationEnabled;
        private InputAction _cameraRotation;
        private InputAction _cameraRadius;

        private void Start() {
            var input = GetComponent<PlayerInput>();
            _cameraRotationEnabled = input.actions.FindAction("Camera Rotation Enabled");
            _cameraRotation = input.actions.FindAction("Camera Rotation");
            _cameraRadius = input.actions.FindAction("Camera Radius");

            _cameraRotation.performed += PerformRotation;
            _cameraRadius.performed += PerformRadius;

            RecalculatePosition();
        }
        
        private void PerformRotation(InputAction.CallbackContext context) {
            if (_cameraRotationEnabled.ReadValue<float>() == 0) return;
            _rotation += context.ReadValue<Vector2>() * 0.1f;
            _rotation.y = Math.Max(Math.Min(_rotation.y, 85), -85);
            RecalculatePosition();
        }

        private void PerformRadius(InputAction.CallbackContext context) {
            _radius += context.ReadValue<float>() > 0 ? -1 : 1;
            RecalculatePosition();
        }

        private void RecalculatePosition() {
            var center = _chunk * 16 + new Vector3Int(8, 8, 8);
            transform.rotation = Quaternion.Euler(-_rotation.y, _rotation.x, 0);
            transform.position = center + -transform.forward * _radius;
        }
    }
}