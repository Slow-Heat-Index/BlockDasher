using System;
using Sources.Identification;
using UnityEngine;

namespace Sources.Level.Skyboxes {
    public class SkyboxWrapper : IIdentifiable {
        public Identifier Identifier { get; }
        public String Name { get; }
        public Material Skybox { get; }

        public SkyboxWrapper(Identifier identifier, string name, Material skybox) {
            Identifier = identifier;
            Name = name;
            Skybox = skybox;
        }
    }
}