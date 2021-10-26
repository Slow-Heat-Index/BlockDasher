using UnityEngine;

namespace Util {
    public class DestroyOnDesktop : MonoBehaviour {
        private void Awake() {
            if (!Application.isMobilePlatform) Destroy(gameObject);
        }
    }
}