using System;
using System.Collections;
using System.Collections.Generic;
using Level.Player.Data;
using UnityEngine;
using UnityEngine.UI;

public class RestartFromUI : MonoBehaviour {
    private Button button;
    private PlayerData playerData;
    private PauseMenu pauseMenu;

    private void Awake() {
        button = GetComponent<Button>();
        playerData = FindObjectOfType<PlayerData>();
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    private void OnEnable() {
        button.onClick.AddListener(Restart);
    }

    private void OnDisable() {
        button.onClick.RemoveListener(Restart);
    }

    void Restart() {
        if (playerData.dead) return;
        
        pauseMenu.RestartLevel();
    }
}
