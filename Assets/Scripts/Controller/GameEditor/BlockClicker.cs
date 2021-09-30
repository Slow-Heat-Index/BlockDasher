using Sources.Identification;
using Sources.Level.Data;
using Sources.Level.Raycast;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller.GameEditor {
    [RequireComponent(typeof(PlayerInput), typeof(Camera))]
    public class BlockClicker : MonoBehaviour {
        private Camera _camera;
        private bool _touchBreak = false;

        private InputAction _removeBlock;
        private InputAction _addBlock;
        private InputAction _mousePosition;

        private InputAction _addRemoveTouch;

        private void Start() {
            _camera = GetComponent<Camera>();

            var input = GetComponent<PlayerInput>();

            _removeBlock = input.actions.FindAction("Remove Block");
            _addBlock = input.actions.FindAction("Add Block");
            _mousePosition = input.actions.FindAction("Mouse Position");
            _addRemoveTouch = input.actions.FindAction("Add Remove Block Touchscreen");

            _removeBlock.performed += BreakBlockMouse;
            _addBlock.performed += PlaceBlockMouse;
            _addRemoveTouch.performed += PlaceBreakBlockTouchscreen;
        }


        private void BreakBlockMouse(InputAction.CallbackContext context) {
            var ray = _camera.ScreenPointToRay(_mousePosition.ReadValue<Vector2>());
            BreakBlock(ray.origin, ray.direction);
        }

        private void PlaceBlockMouse(InputAction.CallbackContext context) {
            var ray = _camera.ScreenPointToRay(_mousePosition.ReadValue<Vector2>());
            PlaceBlock(ray.origin, ray.direction);
        }

        private void PlaceBreakBlockTouchscreen(InputAction.CallbackContext context) {
            var ray = _camera.ScreenPointToRay(_mousePosition.ReadValue<Vector2>());
            if (_touchBreak) {
                BreakBlock(ray.origin, ray.direction);
            }
            else {
                PlaceBlock(ray.origin, ray.direction);
            }
        }

        private void BreakBlock(Vector3 origin, Vector3 direction) {
            var caster = new BlockRaycaster(Test.World, origin, direction, 100);
            caster.Run();
            if (caster.Result != null) {
                var position = caster.Result.Position;
                position.World.PlaceBlock(new BlockData(null), position.Position);
            }
        }

        private void PlaceBlock(Vector3 origin, Vector3 direction) {
            var caster = new BlockRaycaster(Test.World, origin, direction, 100);
            caster.Run();
            if (caster.Result != null) {
                var position = caster.Result.Position;
                position.Move(caster.Face);
                position.World.PlaceBlock(new BlockData(Identifiers.Grass), position.Position);
            }
        }
    }
}