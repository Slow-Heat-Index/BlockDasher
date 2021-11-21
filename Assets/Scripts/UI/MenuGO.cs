using UI.Menu;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI {
    public class MenuGO : MonoBehaviour {
        public static MenuGO Instance { get; private set; }
        private EventSystem _eventSystem;

        private void Start() {
            Instance = this;
            _eventSystem = GetComponentInChildren<EventSystem>();
        }

        public void PrepareMenu() {
            GetComponentInChildren<LevelSelectionManager>().RefreshUIWithLevelData();
            _eventSystem.gameObject.SetActive(true);
        }
    }
}