using Sources.Identification;
using Sources.Level.Skyboxes;
using Sources.Registration;
using UnityEngine;

namespace Sources.Level.Manager {
    public class SkyboxManager : Manager<SkyboxWrapper> {
        public static readonly SkyboxWrapper Garden = new SkyboxWrapper(Identifiers.SkyboxGarden, "Garden",
            Resources.Load<Material>("Skyboxes/Garden/Skybox"), 0);      
        public static readonly SkyboxWrapper Desert = new SkyboxWrapper(Identifiers.SkyboxDesert, "Desert",
            Resources.Load<Material>("Skyboxes/Desert/Skybox"), 1);     
        public static readonly SkyboxWrapper Beach = new SkyboxWrapper(Identifiers.SkyboxBeach, "Beach",
            Resources.Load<Material>("Skyboxes/Beach/Skybox"), 2); 
        public static readonly SkyboxWrapper Castle = new SkyboxWrapper(Identifiers.SkyboxCastle, "Castle",
            Resources.Load<Material>("Skyboxes/Castle/Skybox"), 3);

        public SkyboxManager() : base(Identifiers.ManagerSkybox) {
            Register(Garden);
            Register(Desert);
            Register(Beach);
            Register(Castle);
        }
    }
}