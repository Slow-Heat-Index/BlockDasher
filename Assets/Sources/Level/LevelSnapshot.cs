using Sources.Identification;

namespace Sources.Level {
    public class LevelSnapshot : IIdentifiable {
        public Identifier Identifier { get; }
        public string LevelPath { get; }
        
        public LevelSnapshot(Identifier identifier, string levelPath) {
            Identifier = identifier;
            LevelPath = levelPath;
        }
    }
}