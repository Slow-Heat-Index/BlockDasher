using System;
using System.Collections;
using System.Collections.Generic;
using Level.Cameras.Controller;
using Level.Player.Data;
using UnityEngine;

public class DeathScreen : MonoBehaviour {
    [SerializeField] private GameObject gameplayUI;
    private PlayerData _playerData;
    private LevelCameraController _levelCameraController;
    private AudioSource _audioSource;
    private bool hasStarted = false;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }

    void Start() {
        _playerData = FindObjectOfType<PlayerData>();
        _playerData.onLose += () => gameObject.SetActive(true);

        _levelCameraController = FindObjectOfType<LevelCameraController>();
        
        GetComponent<RectTransform>().localPosition = Vector3.zero;
        
        

        hasStarted = true;


        
        gameObject.SetActive(false);
    }

    private void OnEnable() {
        if (hasStarted) {
            _audioSource.Play();
            DeactivateUI();
        }
        
    }

    void DeactivateUI() {
        if (gameplayUI != null) {
            gameplayUI.SetActive(false);
        }
    }
}
