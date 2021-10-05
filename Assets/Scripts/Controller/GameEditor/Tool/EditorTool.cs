using System.Collections.Generic;
using Sources.Level;
using UnityEngine;

namespace Controller.GameEditor.Tool {
    public interface IEditorTool {

        void Primary(World world, Ray ray);

        void Secondary(World world, Ray ray);
    }
}