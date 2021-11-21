using System;
using Sources.Identification;

namespace Sources.Level {
    [Serializable]
    public struct LevelCompletionData {
        public Identifier level;
        public int steps;
        public int stars;

        public LevelCompletionData(Identifier lvl, int step, int star)
        {
            level = lvl;
            steps = step;
            stars = star;
        }
    }
    
}