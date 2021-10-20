using UnityEngine;

namespace Controller.GameEditor.Tool {
    public interface IEditorTool {
        void Primary(EditorData editorData, Ray ray);

        void Secondary(EditorData editorData, Ray ray);
    }
}