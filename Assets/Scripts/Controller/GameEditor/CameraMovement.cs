using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller.GameEditor {
    public class CameraMovement : ControllerAwareMonoBehaviour<Inputs> {
        protected override Inputs InitInput() => new Inputs();

        private Vector3 _rotation;

        private InputAction _cameraRotationEnabled;
        private InputAction _cameraRotation;
        private InputAction _cameraMovement;
        private InputAction _cameraUpDownMovement;

        private float _speed = 0.1f;

        private Vector3 _target;
        private Vector3 _dampVelocity;

        private void Start() {
            _cameraRotationEnabled = Input.Editor.CameraRotationEnabled;
            _cameraRotation = Input.Editor.CameraRotation;
            _cameraMovement = Input.Editor.CameraMove;
            _cameraUpDownMovement = Input.Editor.CameraUpDown;

            _target = transform.position;

            Input.Editor.CameraSpeed.performed += PerformSpeed;
        }

        private void Update() {
            var read = _cameraMovement.ReadValue<Vector2>() * _speed;
            var upDown = _cameraUpDownMovement.ReadValue<float>() * _speed;

            var t = transform;
            _target += t.forward * read.y + t.right * read.x + Vector3.up * upDown;
            t.position = Vector3.SmoothDamp(t.position, _target, ref _dampVelocity, 0.1f);

            if (_cameraRotationEnabled.ReadValue<float>() == 0) return;

            var delta = _cameraRotation.ReadValue<Vector2>() * 0.2f;
            _rotation += new Vector3(-delta.y, delta.x, 0);

            if (_rotation.x > 85) _rotation.x = 85;
            else if (_rotation.x < -85) _rotation.x = -85;
            t.localEulerAngles = _rotation;
        }

        private void PerformSpeed(InputAction.CallbackContext context) {
            _speed += context.ReadValue<float>() > 0 ? 0.01f : -0.01f;
            _speed = math.clamp(_speed, 0, 0.5f);
        }
    }
}