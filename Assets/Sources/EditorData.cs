using Sources.Level;
using Sources.Level.Blocks;

namespace Sources {
    public static class EditorData {
        public static World World = new World(true);
        
        public static BlockType SelectedBlockType = GrassBlock.GrassBlockType.Instance;
    }
}