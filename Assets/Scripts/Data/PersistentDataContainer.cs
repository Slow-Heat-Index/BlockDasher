using UnityEngine;

namespace Data
{
    public class PersistentDataContainer : MonoBehaviour
    {
        public static PlayerPersistentData PersistentData = null;

        void Awake()
        {
            if (PersistentData != null) {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(this);
            PersistentData = DataAccess.Load() ?? new PlayerPersistentData();
        }
    }
}