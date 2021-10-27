﻿using Sources.Level;

namespace Sources {
    public static class LevelData {
        public static LevelToLoad LevelToLoad = new LevelToLoad("Levels/IntroductoryLevel", true);

        public static void SetLevelToLoad(string path) {
            LevelToLoad = new LevelToLoad(path, true);
        }
    }
}