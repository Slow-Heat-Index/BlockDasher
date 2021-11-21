using Data;
using Sources;
using Sources.Identification;
using Sources.Level;
using Sources.Registration;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Menu {
    public class LevelSelectionManager : MonoBehaviour {
        private RectTransform _rectTransform;
        public int currentWorld;
        private HubMovement _hubMovement;
        private WorldSelection _worldSelection;
        [SerializeField] private GameObject _worlds;
        private HubWorld _hubWorld;

        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _previousButton;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private GameObject _levelData;
        [SerializeField] private GameObject _levelLocked;


        private void Awake() {
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.anchoredPosition = new Vector2(0, -_rectTransform.rect.height);
            _worldSelection = FindObjectOfType<WorldSelection>();
            _previousButton.interactable = false;
            _levelLocked.SetActive(false);
        }

        public void SetLevelSelector() {
            currentWorld = _worldSelection.GetCurrentWorld();
            _hubWorld = _worlds.transform.GetChild(currentWorld).GetComponent<HubWorld>();
            _hubMovement = _hubWorld.GetComponentInChildren<HubWorld>().hubMovement;
            _hubMovement.OnLevelReached.AddListener(ShowUI);
        }

        public void Next() {
            _hubMovement.GoNext();
            _hubWorld.Rotate(_hubMovement.GetCurrentLevel());

            _playButton.interactable = IsUnlocked(_hubMovement.GetCurrentLevel());

            if (_hubMovement.GetCurrentLevel() == _hubMovement.GetNumLevels() - 1) {
                _nextButton.interactable = false;
            }

            _previousButton.interactable = true;
        }

        public void Previous() {
            _hubMovement.GoPrevious();
            _hubWorld.Rotate(_hubMovement.GetCurrentLevel());

            _playButton.interactable = IsUnlocked(_hubMovement.GetCurrentLevel());

            if (_hubMovement.GetCurrentLevel() == 0) {
                _previousButton.interactable = false;
            }

            _nextButton.interactable = true;
        }

        public void HideUI() {
            _nextButton.gameObject.GetComponent<DisappearSmoothly>().Play();
            _previousButton.gameObject.GetComponent<DisappearSmoothly>().Play();
            _playButton.gameObject.GetComponent<DisappearSmoothly>().Play();
            _levelData.GetComponent<DisappearSmoothly>().Play();
            _backButton.GetComponent<DisappearSmoothly>().Play();
            _levelLocked.GetComponent<DisappearSmoothly>().Play();
        }

        public void ShowUI() {
            _nextButton.gameObject.SetActive(true);
            _previousButton.gameObject.SetActive(true);
            _playButton.gameObject.SetActive(true);
            _levelData.SetActive(true);
            _backButton.gameObject.SetActive(true);
            _levelLocked.SetActive(!IsUnlocked(_hubMovement.GetCurrentLevel()));
        }

        public void Play() {
            var manager = Registry.Get<LevelSnapshot>(Identifiers.ManagerLevel);
            var level = manager.Get(new Identifier(_hubWorld.levels[_hubMovement.GetCurrentLevel()]));
            LevelData.SetLevelToLoad(level);

            MenuGO.Instance.gameObject.SetActive(false);
            SceneManager.LoadScene("Level", LoadSceneMode.Additive);
        }

        public bool IsUnlocked(int i) {
            if (i == 0) return true;
            var manager = Registry.Get<LevelSnapshot>(Identifiers.ManagerLevel);
            var level = manager.Get(new Identifier(_hubWorld.levels[i - 1]));
            return PersistentDataContainer.PersistentData.IsCompleted(level.Identifier);
        }
    }
}