using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using Level.Player.Data;
using UnityEngine;
using UnityEngine.UI;

public class ReplayFromDeath : MonoBehaviour {
    [SerializeField] private GameObject gameOverGO;
    [SerializeField] private FullScreenAd ad;
    [SerializeField] private Button menu;
    private Button _button;
    private PauseMenu _pauseMenu;
    private int deaths;

    private void Awake() {
        _button = GetComponent<Button>();
        _pauseMenu = FindObjectOfType<PauseMenu>();
    }

    private void OnEnable() {
        _button.onClick.AddListener(Replay);
        _button.onClick.AddListener(ReplayAd);
    }

    private void OnDisable() {
        _button.onClick.RemoveListener(Replay);
        _button.onClick.RemoveListener(ReplayAd);
    }

    void Replay() {
        _pauseMenu.gameObject.SetActive(true);
        _pauseMenu.RestartLevel();
        _pauseMenu.gameObject.SetActive(false);
        gameOverGO.SetActive(false);
    }

    void ReplayAd() {
        deaths++;
        if (deaths >= 3 && !PersistentDataContainer.PersistentData.adsRemoved) {
            deaths = 0;
            ad.PlayAd();
        }
    }
}
