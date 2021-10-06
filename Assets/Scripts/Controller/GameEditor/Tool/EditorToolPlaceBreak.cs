using Sources;
using Sources.Level;
using Sources.Level.Data;
using Sources.Level.Raycast;
using UnityEngine;

namespace Controller.GameEditor.Tool {
    public class EditorToolPlaceBreak : IEditorTool {
        public void Primary(World world, Ray ray) {
            var caster = new BlockRaycaster(world, ray.origin, ray.direction, 100);
            caster.Run();
            if (caster.Result != null) {
                var position = caster.Result.Position;
                world.PlaceBlock(new BlockData(null), position.Position);
            }
        }

        public void Secondary(World world, Ray ray) {
            var caster = new BlockRaycaster(world, ray.origin, ray.direction, 100);
            caster.Run();
            if (caster.Result != null) {
                var position = caster.Result.Position.Moved(caster.Face);
                if (!EditorData.SelectedBlockType.CanBePlaced(position)) return;
                world.PlaceBlock(new BlockData(EditorData.SelectedBlockType.Identifier), position.Position);
            }
        }
    }
}