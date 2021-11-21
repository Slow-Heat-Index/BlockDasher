using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Skins {
    public class PlayerSkinColor : MonoBehaviour {
        public Color color;
        public Renderer[] renderers;
        public RenderObjects[] renderObjects;

        private void Start() {
            foreach (var renderer in renderers) {
                var alpha = renderer.material.color.a;
                renderer.material.color = new Color(color.r, color.g, color.b, alpha);
            }

            foreach (var render in renderObjects) {
                var alpha = render.settings.overrideMaterial.color.a;
                render.settings.overrideMaterial.color = new Color(color.r, color.g, color.b, alpha);
            }
        }
    }
}