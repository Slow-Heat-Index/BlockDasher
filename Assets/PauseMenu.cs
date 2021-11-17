using System;
using System.Collections;
using System.Collections.Generic;
using Level.Player.Controller;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ScreensTransitions))]
public class PauseMenu : MonoBehaviour {

    [SerializeField] private GameObject _gameplayUIGO;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _replayButton;
    [SerializeField] private Fader _background;
    private ScreensTransitions _screensTransitions;
    private RectTransform _rectTransform;

    private PlayerMovementController _playerController;

    private void Awake() {
        _screensTransitions = GetComponent<ScreensTransitions>();
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.anchoredPosition = new Vector2(0, -_rectTransform.rect.height);
    }
    
    private void OnEnable() {
        _pauseButton.onClick.AddListener(_screensTransitions.ScreenIn);
        _pauseButton.onClick.AddListener(PauseGame);
        
        _continueButton.onClick.AddListener(ContinueGame);
        
    }

    private void OnDisable() {
        _pauseButton.onClick.RemoveListener(_screensTransitions.ScreenIn);
        _pauseButton.onClick.RemoveListener(PauseGame);
        
        _continueButton.onClick.RemoveListener(ContinueGame);
    }

    private void Start() {
        _playerController = FindObjectOfType<PlayerMovementController>();
    }

    

    void PauseGame() {
        _gameplayUIGO.SetActive(false);
        _playerController.enabled = false;
        _background.gameObject.SetActive(true);
        _background.FadeTo(0.4f);
        StartCoroutine(PauseCoroutine());
    }
    
    IEnumerator PauseCoroutine() {
        yield return new WaitForSeconds(_screensTransitions.GetTweenTime());
        Time.timeScale = 0;

    }

    void ContinueGame() {
        _gameplayUIGO.SetActive(true);
        _playerController.enabled = true;
        _background.FadeIn();
        Time.timeScale = 1;
        _screensTransitions.ScreenUp();
        StartCoroutine(ContinueCoroutine());
    }
    
    IEnumerator ContinueCoroutine() {
        yield return new WaitForSeconds(_background.GetTweenTime());
        _background.gameObject.SetActive(false);
    }

}
