using Sources.Identification;
using Sources.Level.Data;
using UnityEngine;

namespace Sources.Level {
    public abstract class BlockType : IIdentifiable {
        public Identifier Identifier { get; }

        public string Name { get; }

        public Mesh DefaultMesh { get; }

        public Material DefaultMaterial { get; }

        protected BlockType(Identifier identifier, string name, Mesh defaultMesh, Material defaultMaterial) {
            Identifier = identifier;
            Name = name;
            DefaultMesh = defaultMesh;
            DefaultMaterial = defaultMaterial;
        }

        public abstract Block CreateBlock(BlockPosition position, BlockData data);
    }
}