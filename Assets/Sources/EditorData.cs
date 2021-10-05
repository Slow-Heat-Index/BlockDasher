using System.Collections.Generic;
using Controller.GameEditor.Tool;
using Sources.Level;
using Sources.Level.Blocks;

namespace Sources {
    public static class EditorData {
        public static readonly Dictionary<EditorToolType, IEditorTool> Types =
            new Dictionary<EditorToolType, IEditorTool> {
                { EditorToolType.PlaceBreak, new EditorToolPlaceBreak() },
                { EditorToolType.Selection, new EditorToolSelection() }
            };

        public static World World = new World(true);

        public static BlockType SelectedBlockType = GrassBlock.GrassBlockType.Instance;
        public static EditorToolType SelectedEditorTool = EditorToolType.PlaceBreak;
    }
}