using System;
using System.Collections;
using Data;
using Level.Cameras.Controller;
using Level.Generator;
using Level.Player.Controller;
using Level.Player.Data;
using Sources;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace UI.Game {
    [RequireComponent(typeof(RectTransform))]
    public class LevelCompletedScreen : MonoBehaviour {
        [SerializeField] private ContinueScreen doubleCoins;
        private PlayerMovementController _playerController;
        private LevelCameraController _levelCameraController;
        private PlayerData _player;
        private LevelGenerator _levelGenerator;
        private PopUpAnim _popUpAnim;
        private RectTransform _rectTransform;
        [SerializeField] private Fader _background;
        private AudioSource _audioSource;

        [SerializeField] private TextMeshProUGUI _steps;
        [SerializeField] private TextMeshProUGUI _time;

        [SerializeField] private Button replayButton;

        private void Awake() {
            doubleCoins = FindObjectOfType<ContinueScreen>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start() {
            _playerController = FindObjectOfType<PlayerMovementController>();
            _levelCameraController = FindObjectOfType<LevelCameraController>();
            _player = FindObjectOfType<PlayerData>();
            _levelGenerator = FindObjectOfType<LevelGenerator>();
            _rectTransform = GetComponent<RectTransform>();
            InitDoubleCoins();
            
            _player.onWin += () => _steps.text = $"Steps: {_player.movements}";
            _player.onWin += () => {
                _background.gameObject.SetActive(true);
                _background.FadeTo(0.4f);
            };
            _player.onWin += () => _playerController.enabled = false;
            _player.onWin += () => _levelCameraController.enabled = false;
            _player.onWin += () => {
                PersistentDataContainer.PersistentData.AddLevelCompleted(LevelData.LevelToLoad.Path,
                    (int)_player.movements, _levelGenerator.World.GoldMoves,_levelGenerator.World.SilverMoves);
                DataAccess.Save(PersistentDataContainer.PersistentData);
            };
            

            _rectTransform.localPosition = Vector3.zero;
            gameObject.SetActive(false);
        }

        private void OnEnable() {
            replayButton.onClick.AddListener(RestartLevel);
            _audioSource.Play();
        }

        private void OnDisable() {
            replayButton.onClick.RemoveListener(RestartLevel);
        }

        private void RestartLevel() {
            _playerController.enabled = true;
            _levelCameraController.enabled = true;
            _background.FadeIn();
            _levelGenerator.World.ResetLevel(true);
            _player.Reset();
            _background.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        void InitDoubleCoins() {
            _player.onWin += () => doubleCoins.gameObject.SetActive(true);
            _player.onWin += doubleCoins.StartCountDown;
            doubleCoins.onCountDownOver += (() => doubleCoins.winGO.SetActive(false));
            doubleCoins.onCountDownOver += () => gameObject.SetActive(true);
            doubleCoins.watchAd.onClick.AddListener(doubleCoins.WatchAd);
        }
    }
}