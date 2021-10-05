using Sources;
using Sources.Level;
using Sources.Level.Raycast;
using Sources.Util;
using UnityEngine;

namespace Controller.GameEditor.Tool {
    [RequireComponent(typeof(MeshRenderer))]
    public class EditorToolSelectionHint : MonoBehaviour {
        public BlockClicker blockClicker;

        private MeshRenderer _renderer;

        private void Start() {
            _renderer = GetComponent<MeshRenderer>();
        }

        private void FixedUpdate() {
            if (EditorData.SelectedEditorTool != EditorToolType.Selection) {
                _renderer.enabled = false;
                return;
            }

            var tool = EditorData.Types[EditorToolType.Selection];
            if (!(tool is EditorToolSelection selection) || selection.ToolStatus == EditorToolSelection.Status.None) {
                _renderer.enabled = false;
                return;
            }

            _renderer.enabled = true;

            var first = selection.Position;
            var secondBlock = Second(out var face);

            if (secondBlock == null) {
                _renderer.enabled = false;
                return;
            }

            var second = secondBlock.Position;

            if (selection.ToolStatus == EditorToolSelection.Status.Secondary) {
                second.Move(face);
            }

            var min = Vector3Int.Min(first, second.Position) + new Vector3(0.5f, 0.5f, 0.5f);
            var max = Vector3Int.Max(first, second.Position) + new Vector3(0.5f, 0.5f, 0.5f);
            var center = (min + max) / 2;
            var scale = center - min;

            var tr = transform;
            tr.position = center;
            tr.localScale = scale * 2 + new Vector3(1.2f, 1.2f, 1.2f);

            _renderer.material.color = selection.ToolStatus == EditorToolSelection.Status.Secondary
                ? new Color(0, 1, 0, 0.5f)
                : new Color(1, 0, 0, 0.5f);
        }


        private Block Second(out Direction face) {
            var ray = blockClicker.Camera.ScreenPointToRay(blockClicker.MousePosition.ReadValue<Vector2>());
            var caster = new BlockRaycaster(EditorData.World, ray.origin, ray.direction, 100);
            caster.Run();
            face = caster.Face;
            return caster.Result;
        }
    }
}