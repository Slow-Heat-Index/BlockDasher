using Sources.Level;
using Sources.Level.Manager;

namespace Sources {
    public static class LevelData {
        public static LevelToLoad LevelToLoad = new LevelToLoad(LevelManager.Level11, true, false);

        public static void SetLevelToLoad(LevelSnapshot level) {
            LevelToLoad = new LevelToLoad(level, true, level.Tutorial);
        }
    }
}