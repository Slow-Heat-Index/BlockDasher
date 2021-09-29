using UnityEngine;
using UnityEngine.InputSystem;

namespace Cameras {
    [RequireComponent(typeof(PlayerInput))]
    public class ChunkCameraFixer : MonoBehaviour {
        private Vector3Int _chunk = new Vector3Int(0, 0, 0);

        private InputAction _cameraRotation;

        private void Start() {
            var input = GetComponent<PlayerInput>();
            _cameraRotation = input.actions.FindAction("Camera Rotation");
            _cameraRotation.performed += test => {
                print(test.valueType);
                print(test.ReadValueAsObject());
            };
        }

        private void Update() {
            //print(_cameraRotation.enabled);
            //print(_cameraRotation.ReadValue<Vector2>());
        }
    }
}