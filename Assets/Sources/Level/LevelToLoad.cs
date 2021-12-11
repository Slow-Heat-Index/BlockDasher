using System.IO;
using UnityEngine;

namespace Sources.Level {
    public struct LevelToLoad {
        public LevelSnapshot Level;
        public bool LoadFromResources;
        public bool LoadTutorial;

        public LevelToLoad(LevelSnapshot level, bool loadFromResources, bool loadTutorial) {
            Level = level;
            LoadFromResources = loadFromResources;
            LoadTutorial = loadTutorial;
        }

        public void Load(World world) {
            BinaryReader reader;
            if (LoadFromResources) {
                var asset = Resources.Load<TextAsset>(Level.LevelPath);
                var stream = new MemoryStream(asset.bytes);
                reader = new BinaryReader(stream);
            }
            else {
                reader = new BinaryReader(File.OpenRead(Level.LevelPath));
            }

            using (reader) {
                world.Read(reader);
            }
        }
    }
}