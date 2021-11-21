using System.Linq;
using Data;
using Sources;
using Sources.Identification;
using Sources.Level;
using Sources.Registration;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Menu {
    public class LevelSelectionManager : MonoBehaviour {
        private RectTransform _rectTransform;
        public int currentWorld;
        private HubMovement _hubMovement;
        private WorldSelection _worldSelection;
        private HubWorld _hubWorld;

        [SerializeField] private LoadingSceneAnim loadingSceneAnim;
        [SerializeField] private EventSystem eventSystem;
        [SerializeField] private GameObject worlds;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button previousButton;
        [SerializeField] private Button playButton;
        [SerializeField] private Button backButton;
        [SerializeField] private GameObject levelData;
        [SerializeField] private GameObject levelLocked;

        [Header("Level info")] [SerializeField]
        private TextMeshProUGUI levelName;

        [SerializeField] private TextMeshProUGUI levelHighScore;
        [SerializeField] private Image levelStars;

        [Header("Resources")] [SerializeField] private Sprite[] stars;


        private void Awake() {
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.anchoredPosition = new Vector2(0, -_rectTransform.rect.height);
            _worldSelection = FindObjectOfType<WorldSelection>();
            previousButton.interactable = false;
            levelLocked.SetActive(false);
        }

        public void SetLevelSelector() {
            currentWorld = _worldSelection.GetCurrentWorld();
            _hubWorld = worlds.transform.GetChild(currentWorld).GetComponent<HubWorld>();
            _hubMovement = _hubWorld.GetComponentInChildren<HubWorld>().hubMovement;
            _hubMovement.OnLevelReached.AddListener(ShowUI);
            RefreshUIWithLevelData();
        }

        public void Next() {
            _hubMovement.GoNext();
            RefreshUIWithLevelData();
        }

        public void Previous() {
            _hubMovement.GoPrevious();
            RefreshUIWithLevelData();
        }

        public void HideUI() {
            nextButton.gameObject.GetComponent<DisappearSmoothly>().Play();
            previousButton.gameObject.GetComponent<DisappearSmoothly>().Play();
            playButton.gameObject.GetComponent<DisappearSmoothly>().Play();
            levelData.GetComponent<DisappearSmoothly>().Play();
            backButton.GetComponent<DisappearSmoothly>().Play();
            levelLocked.GetComponent<DisappearSmoothly>().Play();
        }

        public void ShowUI() {
            nextButton.gameObject.SetActive(true);
            previousButton.gameObject.SetActive(true);
            playButton.gameObject.SetActive(true);
            levelData.SetActive(true);
            backButton.gameObject.SetActive(true);
            levelLocked.SetActive(!IsUnlocked(_hubMovement.GetCurrentLevel()));
        }

        public void Play() {
            var manager = Registry.Get<LevelSnapshot>(Identifiers.ManagerLevel);
            var level = manager.Get(new Identifier(_hubWorld.levels[_hubMovement.GetCurrentLevel()]));
            LevelData.SetLevelToLoad(level);

            loadingSceneAnim.gameObject.SetActive(true);
            eventSystem.gameObject.SetActive(false);
            var coroutine = StartCoroutine(loadingSceneAnim.LoadingAnim());
            var async = SceneManager.LoadSceneAsync("Level", LoadSceneMode.Additive);

            async.completed += o => {
                loadingSceneAnim.gameObject.SetActive(false);
                MenuGO.Instance.gameObject.SetActive(false);
                StopCoroutine(coroutine);
            };
        }

        public bool IsUnlocked(int i) {
            if (i == 0) return true;
            var manager = Registry.Get<LevelSnapshot>(Identifiers.ManagerLevel);
            var level = manager.Get(new Identifier(_hubWorld.levels[i - 1]));
            return PersistentDataContainer.PersistentData.IsCompleted(level.Identifier);
        }

        public void RefreshUIWithLevelData() {
            var current = _hubMovement.GetCurrentLevel();
            var unlocked = IsUnlocked(_hubMovement.GetCurrentLevel());

            _hubWorld.Rotate(_hubMovement.GetCurrentLevel());

            playButton.interactable = unlocked;

            previousButton.interactable = current > 0;
            nextButton.interactable = current < _hubMovement.GetNumLevels() - 1;

            levelLocked.SetActive(!unlocked);

            RefreshLevelInfo();
        }

        private void RefreshLevelInfo() {
            var manager = Registry.Get<LevelSnapshot>(Identifiers.ManagerLevel);
            var level = manager.Get(new Identifier(_hubWorld.levels[_hubMovement.GetCurrentLevel()]));

            levelName.text = $"Level {_hubMovement.GetCurrentLevel() + 1}";

            var completedLevels = PersistentDataContainer.PersistentData.completedLevels;
            if (completedLevels.Any(it => it.level == level.Identifier)) {
                var data = PersistentDataContainer.PersistentData.completedLevels
                    .Find(it => it.level == level.Identifier);
                levelHighScore.text = data.steps.ToString();
                levelStars.sprite = stars[data.stars];
            }
            else {
                levelHighScore.text = "-";
                levelStars.sprite = stars[0];
            }
        }
    }
}