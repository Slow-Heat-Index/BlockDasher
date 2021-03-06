using Sources.Level;
using Sources.Level.Raycast;
using Sources.Util;
using UnityEngine;

namespace Controller.GameEditor.Tool {
    [RequireComponent(typeof(MeshRenderer))]
    public class EditorToolHint : MonoBehaviour {
        public BlockClicker blockClicker;

        private MeshRenderer _renderer;

        private void Start() {
            _renderer = GetComponent<MeshRenderer>();
        }

        private void FixedUpdate() {
            switch (blockClicker.EditorData.selectedEditorTool) {
                case EditorToolType.PlaceBreak:
                    HintPlaceBreakTool();
                    break;
                case EditorToolType.Selection:
                    HintSelectionTool();
                    break;
                default:
                    _renderer.enabled = false;
                    break;
            }
        }

        private void HintPlaceBreakTool() {
            var tool = EditorData.Tools[EditorToolType.PlaceBreak];
            if (!(tool is EditorToolPlaceBreak placeBreak)) {
                _renderer.enabled = false;
                return;
            }

            var target = getTargetBlock(out _);
            if (target == null) {
                _renderer.enabled = false;
                return;
            }

            var min = target.CollisionBox.Min + target.Position.Position;
            var max = target.CollisionBox.Max + target.Position.Position;
            var center = (max + min) / 2;
            var scale = (center - min) * 2.05f;

            _renderer.enabled = true;

            var tr = transform;
            tr.position = center;
            tr.localScale = scale;
            _renderer.material.color = new Color(1, 1, 0, 0.2f);
        }

        private void HintSelectionTool() {
            var tool = EditorData.Tools[EditorToolType.Selection];
            if (!(tool is EditorToolSelection selection) || selection.ToolStatus == EditorToolSelection.Status.None) {
                _renderer.enabled = false;
                return;
            }

            var first = selection.Position;
            var target = getTargetBlock(out var face);

            if (target == null) {
                _renderer.enabled = false;
                return;
            }

            _renderer.enabled = true;

            var second = target.Position;

            if (selection.ToolStatus == EditorToolSelection.Status.Secondary) {
                second = second.Moved(face);
            }

            var min = Vector3Int.Min(first, second.Position) + new Vector3(0.5f, 0.5f, 0.5f);
            var max = Vector3Int.Max(first, second.Position) + new Vector3(0.5f, 0.5f, 0.5f);
            var center = (min + max) / 2;
            var scale = center - min;

            var tr = transform;
            tr.position = center;
            tr.localScale = scale * 2 + new Vector3(1.02f, 1.02f, 1.02f);

            _renderer.material.color = selection.ToolStatus == EditorToolSelection.Status.Secondary
                ? new Color(0, 1, 0, 0.5f)
                : new Color(1, 0, 0, 0.5f);
        }


        private Block getTargetBlock(out Direction face) {
            var ray = blockClicker.Camera.ScreenPointToRay(blockClicker.MousePosition.ReadValue<Vector2>());
            var caster = new BlockRaycaster(blockClicker.EditorData.World, ray.origin, ray.direction, 100);
            caster.Run();
            face = caster.Face;
            return caster.Result;
        }
    }
}