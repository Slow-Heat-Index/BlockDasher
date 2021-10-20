using Sources.Level.Data;
using Sources.Level.Raycast;
using UnityEngine;

namespace Controller.GameEditor.Tool {
    public class EditorToolPlaceBreak : IEditorTool {
        public void Primary(EditorData editorData, Ray ray) {
            var caster = new BlockRaycaster(editorData.World, ray.origin, ray.direction, 100);
            caster.Run();
            if (caster.Result != null) {
                var position = caster.Result.Position;
                editorData.World.PlaceBlock(new BlockData(null), position.Position, true);
            }
        }

        public void Secondary(EditorData editorData, Ray ray) {
            var caster = new BlockRaycaster(editorData.World, ray.origin, ray.direction, 100);
            caster.Run();
            if (caster.Result != null) {
                var position = caster.Result.Position.Moved(caster.Face);
                if (!editorData.SelectedBlockType.CanBePlaced(position)) return;
                editorData.World.PlaceBlock(
                    new BlockData(editorData.SelectedBlockType.Identifier, editorData.Metadata),
                    position.Position,
                    true
                );
            }
        }
    }
}