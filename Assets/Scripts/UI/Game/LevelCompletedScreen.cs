using System.Collections;
using Data;
using Level.Generator;
using Level.Player.Controller;
using Level.Player.Data;
using Sources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game {
    [RequireComponent(typeof(ScreensTransitions), typeof(RectTransform))]
    public class LevelCompletedScreen : MonoBehaviour {
        private PlayerMovementController _playerController;
        private PlayerData _player;
        private LevelGenerator _levelGenerator;
        private ScreensTransitions _screensTransitions;
        private RectTransform _rectTransform;
        [SerializeField] private Fader _background;

        [SerializeField] private TextMeshProUGUI _steps;
        [SerializeField] private TextMeshProUGUI _time;

        [SerializeField] private Button replayButton;


        private void Start() {
            _playerController = FindObjectOfType<PlayerMovementController>();
            _player = FindObjectOfType<PlayerData>();
            _levelGenerator = FindObjectOfType<LevelGenerator>();
            _screensTransitions = GetComponent<ScreensTransitions>();
            _rectTransform = GetComponent<RectTransform>();

            _player.onWin += _screensTransitions.ScreenIn;
            _player.onWin += () => _steps.text = $"Steps: {_player.movements}";
            _player.onWin += () => {
                _background.gameObject.SetActive(true);
                _background.FadeTo(0.4f);
            };
            _player.onWin += () => _playerController.enabled = false;
            _player.onWin += () => {
                PersistentDataContainer.PersistentData.AddLevelCompleted(LevelData.LevelToLoad.Path,
                    (int)_player.movements, _levelGenerator.World.GoldMoves,_levelGenerator.World.SilverMoves);
                DataAccess.Save(PersistentDataContainer.PersistentData);
            };

            _rectTransform.anchoredPosition = new Vector2(0, -_rectTransform.rect.height);
        }

        private void OnEnable() {
            replayButton.onClick.AddListener(RestartLevel);
        }

        private void OnDisable() {
            replayButton.onClick.RemoveListener(RestartLevel);
        }

        private void RestartLevel() {
            _playerController.enabled = true;
            _background.FadeIn();
            _screensTransitions.ScreenDown();
            _levelGenerator.World.ResetLevel(true);
            _player.Reset();
            StartCoroutine(ContinueCoroutine());
        }

        IEnumerator ContinueCoroutine() {
            yield return new WaitForSeconds(_background.GetTweenTime());
            _background.gameObject.SetActive(false);
        }
    }
}