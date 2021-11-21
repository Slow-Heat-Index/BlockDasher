using Sources.Identification;

namespace Sources.Skins {
    public class Skin : IIdentifiable {
        public Identifier Identifier { get; }

        public string Name { get; }

        public string PrefabPath { get; }

        public Skin(Identifier identifier, string name, string prefabPath) {
            Identifier = identifier;
            Name = name;
            PrefabPath = prefabPath;
        }
    }
}