using System;
using System.Collections.Generic;
using Controller.GameEditor.Tool;
using Sources.Level;
using Sources.Level.Blocks;

namespace Sources {
    public static class EditorData {
        public static readonly Dictionary<EditorToolType, IEditorTool> Tools =
            new Dictionary<EditorToolType, IEditorTool> {
                { EditorToolType.PlaceBreak, new EditorToolPlaceBreak() },
                { EditorToolType.Selection, new EditorToolSelection() }
            };

        public static World World = new World(true);

        private static BlockType _selectedBlockType = GrassBlock.GrassBlockType.Instance;
        private static Dictionary<string, string> _metadata = new Dictionary<string, string>();

        public static BlockType SelectedBlockType {
            get => _selectedBlockType;
            set {
                _selectedBlockType = value;
                OnSelectedBlockTypeChange?.Invoke(value);
            }
        }

        public static Dictionary<string, string> Metadata {
            get => _metadata;
            set {
                _metadata = value;
                OnMetadataChange?.Invoke(value);
            }
        }

        public static EditorToolType SelectedEditorTool = EditorToolType.PlaceBreak;

        /**
         * Event called when the selected block type is changed.
         */
        public static event Action<BlockType> OnSelectedBlockTypeChange;

        /**
         * Event called when the whole metadata map is changed.
         * This event doesn't fire if any of the values inside the dictionary is changed.
         */
        public static event Action<Dictionary<string, string>> OnMetadataChange;
    }
}