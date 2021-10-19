using UnityEngine;

namespace Util {
    public class DestroyOnDesktop : MonoBehaviour {
        private void Awake() {
            if (SystemInfo.deviceType == DeviceType.Desktop) Destroy(gameObject);
        }
    }
}