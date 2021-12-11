using Sources.Identification;

namespace Sources.Level {
    public class LevelSnapshot : IIdentifiable {
        public Identifier Identifier { get; }
        public string LevelPath { get; }
        public Identifier NextLevel { get; }
        public bool Tutorial { get; }
        
        public LevelSnapshot(Identifier identifier, string levelPath,
            Identifier nextLevel = null, bool tutorial = false) {
            Identifier = identifier;
            LevelPath = levelPath;
            NextLevel = nextLevel;
            Tutorial = tutorial;
        }
    }
}