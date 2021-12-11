using Sources.Identification;

namespace Sources.Level {
    public class LevelSnapshot : IIdentifiable {
        public Identifier Identifier { get; }
        public string LevelPath { get; }
        
        public bool Tutorial { get; }
        
        public LevelSnapshot(Identifier identifier, string levelPath, bool tutorial = false) {
            Identifier = identifier;
            LevelPath = levelPath;
            Tutorial = tutorial;
        }
    }
}