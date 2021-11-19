using System;
using System.Collections;
using System.Collections.Generic;
using Level.Player.Data;
using UnityEngine;
using UnityEngine.UI;

public class ReplayFromDeath : MonoBehaviour {
    [SerializeField] private ScreensTransitions _screensTransitions;
    private Button _button;
    private PauseMenu _pauseMenu;

    private void Awake() {
        _button = GetComponent<Button>();
        _pauseMenu = FindObjectOfType<PauseMenu>();
    }

    private void OnEnable() {
        _button.onClick.AddListener(_pauseMenu.RestartLevel);
        _button.onClick.AddListener(_screensTransitions.ScreenUp);
    }

    private void OnDisable() {
        _button.onClick.RemoveListener(_pauseMenu.RestartLevel);
        _button.onClick.RemoveListener(_screensTransitions.ScreenUp);
    }
}
