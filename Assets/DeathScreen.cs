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
    private bool started;


    void Start() {
        _playerData = FindObjectOfType<PlayerData>();
        _playerData.onLose += () => gameObject.SetActive(true);

        _levelCameraController = FindObjectOfType<LevelCameraController>();
        _audioSource = GetComponent<AudioSource>();
        
        GetComponent<RectTransform>().localPosition = Vector3.zero;
        _levelCameraController.enabled = false;
        gameObject.SetActive(false);
    }

    private void OnEnable() {
        if (started) {
            gameplayUI.SetActive(false);
        }
        else {
            started = true;
        }
        
        _audioSource.Play();
    }
}
