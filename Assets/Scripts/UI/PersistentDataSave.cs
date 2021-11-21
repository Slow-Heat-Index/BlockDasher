using Data;
using UnityEngine;

namespace UI {
    public class PersistentDataSave : MonoBehaviour {
        public void Save() {
            DataAccess.Save(PersistentDataContainer.PersistentData);
        }
    }
}