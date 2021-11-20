using System;
using System.Collections;
using System.Collections.Generic;
using Level.Cameras.Controller;
using Level.Generator;
using Level.Player.Controller;
using Level.Player.Data;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour {

    [SerializeField] private GameObject gameplayUigo;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button replayButton;
    [SerializeField] private Fader background;
    private PopUpAnim _popUpAnim;
    private RectTransform _rectTransform;
    private PlayerMovementController _playerController;
    private LevelCameraController _levelCameraController;
    private LevelGenerator _levelGenerator;
    private PlayerData _player;

    private void Awake() {
        _popUpAnim = GetComponent<PopUpAnim>();
        _rectTransform = GetComponent<RectTransform>();

        _rectTransform.localPosition = Vector3.zero;
    }
    
    private void OnEnable() {
        pauseButton.onClick.AddListener(PauseGame);
        continueButton.onClick.AddListener(ContinueGame);
        replayButton.onClick.AddListener(RestartLevel);
    }

    private void OnDisable() {
        pauseButton.onClick.RemoveListener(PauseGame);
        continueButton.onClick.RemoveListener(ContinueGame);
        replayButton.onClick.RemoveListener(RestartLevel);
    }

    private void Start() {
        _playerController = FindObjectOfType<PlayerMovementController>();
        _levelGenerator = FindObjectOfType<LevelGenerator>();
        _player = FindObjectOfType<PlayerData>();
        _levelCameraController = FindObjectOfType<LevelCameraController>();
        
        gameObject.SetActive(false);
    }

    

    public void PauseGame() {
        gameplayUigo.SetActive(false);
        _playerController.enabled = false;
        _levelCameraController.enabled = false;
        background.gameObject.SetActive(true);
        background.FadeTo(0.4f);
        Time.timeScale = 0;
    }


    void ContinueGame() {
        gameplayUigo.SetActive(true);
        _playerController.enabled = true;
        _levelCameraController.enabled = true;
        background.FadeIn();
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    
    public void RestartLevel() {
        gameplayUigo.SetActive(true);
        _playerController.enabled = true;
        _levelCameraController.enabled = true;
        background.FadeIn();
        Time.timeScale = 1;
        _levelGenerator.World.ResetLevel(true);
        _player.Reset();
        gameObject.SetActive(false);
    }


}
