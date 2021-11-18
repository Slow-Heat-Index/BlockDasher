using System;
using Sources.Identification;

namespace Sources.Level {
    [Serializable]
    public struct LevelCompletionData {
        public string level;
        public int steps;
        public int stars;

        public LevelCompletionData(string lvl, int step, int star)
        {
            level = lvl;
            steps = step;
            stars = star;
        }
    }
    
}