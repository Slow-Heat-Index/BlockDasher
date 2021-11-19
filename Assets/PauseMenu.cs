using System;
using System.Collections;
using System.Collections.Generic;
using Level.Generator;
using Level.Player.Controller;
using Level.Player.Data;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ScreensTransitions))]
public class PauseMenu : MonoBehaviour {

    [SerializeField] private GameObject gameplayUigo;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button replayButton;
    [SerializeField] private Fader background;
    private ScreensTransitions _screensTransitions;
    private RectTransform _rectTransform;
    private PlayerMovementController _playerController;
    private LevelGenerator _levelGenerator;
    private PlayerData _player;

    private void Awake() {
        _screensTransitions = GetComponent<ScreensTransitions>();
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.anchoredPosition = new Vector2(0, _rectTransform.rect.height);
    }
    
    private void OnEnable() {
        pauseButton.onClick.AddListener(_screensTransitions.ScreenIn);
        pauseButton.onClick.AddListener(PauseGame);
        continueButton.onClick.AddListener(ContinueGame);
        replayButton.onClick.AddListener(RestartLevel);
    }

    private void OnDisable() {
        pauseButton.onClick.RemoveListener(_screensTransitions.ScreenIn);
        pauseButton.onClick.RemoveListener(PauseGame);
        continueButton.onClick.RemoveListener(ContinueGame);
        replayButton.onClick.RemoveListener(RestartLevel);
    }

    private void Start() {
        _playerController = FindObjectOfType<PlayerMovementController>();
        _levelGenerator = FindObjectOfType<LevelGenerator>();
        _player = FindObjectOfType<PlayerData>();
    }

    

    void PauseGame() {
        gameplayUigo.SetActive(false);
        _playerController.enabled = false;
        background.gameObject.SetActive(true);
        background.FadeTo(0.4f);
        StartCoroutine(PauseCoroutine());
    }
    
    IEnumerator PauseCoroutine() {
        yield return new WaitForSeconds(_screensTransitions.GetTweenTime());
        Time.timeScale = 0;

    }

    void ContinueGame() {
        gameplayUigo.SetActive(true);
        _playerController.enabled = true;
        background.FadeIn();
        Time.timeScale = 1;
        _screensTransitions.ScreenUp();
        StartCoroutine(ContinueCoroutine());
    }
    
    public void RestartLevel() {
        gameplayUigo.SetActive(true);
        _playerController.enabled = true;
        background.FadeIn();
        Time.timeScale = 1;
        _screensTransitions.ScreenUp();
        _levelGenerator.World.ResetLevel(true);
        _player.Reset();
        StartCoroutine(ContinueCoroutine());
    }
    
    IEnumerator ContinueCoroutine() {
        yield return new WaitForSeconds(background.GetTweenTime());
        background.gameObject.SetActive(false);
    }

}
