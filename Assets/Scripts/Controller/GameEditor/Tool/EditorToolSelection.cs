using System;
using Sources.Level;
using Sources.Level.Data;
using Sources.Level.Raycast;
using UnityEngine;

namespace Controller.GameEditor.Tool {
    public class EditorToolSelection : IEditorTool {
        public Vector3Int Position { get; private set; }
        public Status ToolStatus { get; private set; }

        public void Primary(EditorData editorData, Ray ray) {
            switch (ToolStatus) {
                case Status.None:
                case Status.Primary:
                    var caster = new BlockRaycaster(editorData.World, ray.origin, ray.direction, 100);
                    caster.Run();
                    if (caster.Result != null) {
                        if (ToolStatus == Status.None) {
                            ToolStatus = Status.Primary;
                            Position = caster.Result.Position.Position;
                        }
                        else {
                            ToolStatus = Status.None;
                            Fill(null, Position, caster.Result.Position.Position, editorData);
                        }
                    }

                    break;
                case Status.Secondary:
                    ToolStatus = Status.None;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Secondary(EditorData editorData, Ray ray) {
            switch (ToolStatus) {
                case Status.None:
                case Status.Secondary:
                    var caster = new BlockRaycaster(editorData.World, ray.origin, ray.direction, 100);
                    caster.Run();
                    if (caster.Result != null) {
                        var position = caster.Result.Position.Moved(caster.Face);

                        if (ToolStatus == Status.None) {
                            ToolStatus = Status.Secondary;
                            Position = position.Position;
                        }
                        else {
                            ToolStatus = Status.None;
                            Fill(editorData.SelectedBlockType, Position, position.Position, editorData);
                        }
                    }

                    break;
                case Status.Primary:
                    ToolStatus = Status.None;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Fill(BlockType blockType, Vector3Int from, Vector3Int to, EditorData editorData) {
            var data = new BlockData(blockType?.Identifier);

            var min = Vector3Int.Min(from, to);
            var max = Vector3Int.Max(from, to);

            for (var y = min.y; y <= max.y; y++) {
                for (var x = min.x; x <= max.x; x++) {
                    for (var z = min.z; z <= max.z; z++) {
                        var position = new BlockPosition(editorData.World, new Vector3Int(x, y, z));
                        if (blockType != null && !blockType.CanBePlaced(position)) return;
                        editorData.World.PlaceBlock(
                            new BlockData(blockType?.Identifier, editorData.Metadata),
                            position.Position,
                            true
                        );
                    }
                }
            }
        }

        public enum Status {
            None,
            Primary,
            Secondary
        }
    }
}