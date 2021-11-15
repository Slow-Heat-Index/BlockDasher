using Sources.Identification;
using Sources.Level.Skyboxes;
using Sources.Registration;
using UnityEngine;

namespace Sources.Level.Manager {
    public class SkyboxManager : Manager<SkyboxWrapper> {
        public static readonly SkyboxWrapper Garden = new SkyboxWrapper(Identifiers.SkyboxGarden, "Garden",
            Resources.Load<Material>("Skyboxes/Garden/Skybox"));      
        public static readonly SkyboxWrapper Desert = new SkyboxWrapper(Identifiers.SkyboxDesert, "Desert",
            Resources.Load<Material>("Skyboxes/Desert/Skybox"));     
        public static readonly SkyboxWrapper Beach = new SkyboxWrapper(Identifiers.SkyboxBeach, "Beach",
            Resources.Load<Material>("Skyboxes/Beach/Skybox")); 
        public static readonly SkyboxWrapper Castle = new SkyboxWrapper(Identifiers.SkyboxCastle, "Castle",
            Resources.Load<Material>("Skyboxes/Castle/Skybox"));

        public SkyboxManager() : base(Identifiers.ManagerSkybox) {
            Register(Garden);
            Register(Desert);
            Register(Beach);
            Register(Castle);
        }
    }
}