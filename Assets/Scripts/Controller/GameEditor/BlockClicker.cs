using Sources;
using Sources.Identification;
using Sources.Level.Data;
using Sources.Level.Raycast;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace Controller.GameEditor {
    [RequireComponent(typeof(Camera))]
    public class BlockClicker : ControllerAwareMonoBehaviour<Inputs> {
        protected override Inputs InitInput() => new Inputs();

        public InputSystemUIInputModule inputModule;

        private Camera _camera;

        private InputAction _removeBlock;
        private InputAction _addBlock;
        private InputAction _mousePosition;

        private void Start() {
            _camera = GetComponent<Camera>();

            _removeBlock = Input.Editor.RemoveBlock;
            _addBlock = Input.Editor.AddBlock;
            _mousePosition = Input.Editor.MousePosition;

            _removeBlock.performed += BreakBlockMouse;
            _addBlock.performed += PlaceBlockMouse;
        }


        private void BreakBlockMouse(InputAction.CallbackContext context) {
            if(inputModule.IsPointerOverGameObject(-1)) return;
            var ray = _camera.ScreenPointToRay(_mousePosition.ReadValue<Vector2>());
            BreakBlock(ray.origin, ray.direction);
        }

        private void PlaceBlockMouse(InputAction.CallbackContext context) {
            if(inputModule.IsPointerOverGameObject(-1)) return;
            var ray = _camera.ScreenPointToRay(_mousePosition.ReadValue<Vector2>());
            PlaceBlock(ray.origin, ray.direction);
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
                position.World.PlaceBlock(new BlockData(EditorData.BlockType.Identifier), position.Position);
            }
        }
    }
}