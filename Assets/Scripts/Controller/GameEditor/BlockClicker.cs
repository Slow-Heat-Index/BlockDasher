using Controller.GameEditor.Tool;
using Sources.Level.Raycast;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace Controller.GameEditor {
    [RequireComponent(typeof(Camera))]
    public class BlockClicker : ControllerAwareMonoBehaviour<Inputs> {
        protected override Inputs InitInput() => new Inputs();

        public InputSystemUIInputModule inputModule;

        public Camera Camera { get; private set; }

        public InputAction MousePosition { get; private set; }

        public EditorData EditorData { get; private set; }

        private void Start() {
            Camera = GetComponent<Camera>();
            EditorData = FindObjectOfType<EditorData>();

            MousePosition = Input.Editor.MousePosition;

            Input.Editor.RemoveBlock.performed += PrimaryMouse;
            Input.Editor.AddBlock.performed += SecondaryMouse;
            Input.Editor.PickBlock.performed += PickBlockMouse;
            Input.Editor.ChangeTool.performed += ChangeTool;
        }


        private void PrimaryMouse(InputAction.CallbackContext context) {
            if (inputModule.IsPointerOverGameObject(-1)) return;
            var ray = Camera.ScreenPointToRay(MousePosition.ReadValue<Vector2>());
            EditorData.Tools[EditorData.selectedEditorTool].Primary(EditorData, ray);
        }

        private void SecondaryMouse(InputAction.CallbackContext context) {
            if (inputModule.IsPointerOverGameObject(-1)) return;
            var ray = Camera.ScreenPointToRay(MousePosition.ReadValue<Vector2>());
            EditorData.Tools[EditorData.selectedEditorTool].Secondary(EditorData, ray);
        }

        private void PickBlockMouse(InputAction.CallbackContext context) {
            if (inputModule.IsPointerOverGameObject(-1)) return;
            var ray = Camera.ScreenPointToRay(MousePosition.ReadValue<Vector2>());
            PickBlock(ray.origin, ray.direction);
        }

        private void ChangeTool(InputAction.CallbackContext context) {
            var tool = EditorData.selectedEditorTool;
            if (tool == EditorToolType.Selection) tool = 0;
            else tool++;
            EditorData.selectedEditorTool = tool;
        }

        private void PickBlock(Vector3 origin, Vector3 direction) {
            var caster = new BlockRaycaster(EditorData.World, origin, direction, 100);
            caster.Run();
            if (caster.Result != null) {
                EditorData.SelectedBlockType = caster.Result.BlockType;
                EditorData.Metadata = caster.Result.GetMetadataCopy();
            }
        }
    }
}