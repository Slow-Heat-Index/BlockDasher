using System.IO;
using UnityEngine;

namespace Sources.Level {
    public struct LevelToLoad {
        public string Path;
        public bool LoadFromResources;

        public LevelToLoad(string path, bool loadFromResources) {
            Path = path;
            LoadFromResources = loadFromResources;
        }

        public void Load(World world) {
            BinaryReader reader;
            if (LoadFromResources) {
                var asset = Resources.Load<TextAsset>(Path);
                var stream = new MemoryStream(asset.bytes);
                reader = new BinaryReader(stream);
            }
            else {
                reader = new BinaryReader(File.OpenRead(Path));
            }

            using (reader) {
                world.Read(reader);
            }
        }
    }
}