using System;
using System.Collections;
using System.Collections.Generic;
using Level.Player.Data;
using Sources;
using Sources.Level;
using UnityEngine;

public class TutorialDisplay : MonoBehaviour {
    [SerializeField] private GameObject pcTuto;
    [SerializeField] private GameObject mobileTuto;
    private PlayerData player;
    private DisappearSmoothly disappearSmoothly;

    private void Awake() {
        disappearSmoothly = GetComponent<DisappearSmoothly>();
    }

    private void Start() {
        if (!LevelData.LevelToLoad.LoadTutorial) return;
        
        if (Application.isMobilePlatform) {
            mobileTuto.SetActive(true);
        }
        else {
            pcTuto.SetActive(true);
        }
        
        player = FindObjectOfType<PlayerData>();
        player.onMove += DisableTutorial;
    }

    void DisableTutorial() {
        disappearSmoothly.Play();
    }
}
