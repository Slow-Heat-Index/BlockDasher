using System;
using System.Collections;
using System.Collections.Generic;
using Level.Cameras.Controller;
using Level.Player.Data;
using UnityEngine;

public class DeathScreen : MonoBehaviour {
    private PlayerData _playerData;
    private LevelCameraController _levelCameraController;
    private AudioSource _audioSource;
    void Start() {
        _playerData = FindObjectOfType<PlayerData>();
        _playerData.onLose += () => gameObject.SetActive(true);

        _levelCameraController = FindObjectOfType<LevelCameraController>();
        _audioSource = GetComponent<AudioSource>();
        
        GetComponent<RectTransform>().localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }

    private void OnEnable() {
        _levelCameraController.enabled = false;
        _audioSource.Play();
    }
}
