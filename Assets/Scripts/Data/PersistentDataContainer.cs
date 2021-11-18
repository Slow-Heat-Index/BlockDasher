using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Data
{
    public class PersistentDataContainer : MonoBehaviour
    {
        public static PlayerPersistentData PersistentData = null;

        void Start()
        {
            DontDestroyOnLoad(this);
            PersistentData = DataAccess.Load() ?? new PlayerPersistentData();
        }
    }
}