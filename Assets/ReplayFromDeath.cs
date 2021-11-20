using System;
using System.Collections;
using System.Collections.Generic;
using Level.Player.Data;
using UnityEngine;
using UnityEngine.UI;

public class ReplayFromDeath : MonoBehaviour {
    [SerializeField] private GameObject gameOverGO;
    private Button _button;
    private PauseMenu _pauseMenu;

    private void Awake() {
        _button = GetComponent<Button>();
        _pauseMenu = FindObjectOfType<PauseMenu>();
    }

    private void OnEnable() {
        _button.onClick.AddListener(Replay);
    }

    private void OnDisable() {
        _button.onClick.RemoveListener(Replay);
    }

    void Replay() {
        _pauseMenu.gameObject.SetActive(true);
        _pauseMenu.RestartLevel();
        _pauseMenu.gameObject.SetActive(false);
        gameOverGO.SetActive(false);
    }
}
