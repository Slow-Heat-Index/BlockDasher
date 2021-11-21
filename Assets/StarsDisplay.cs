using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using Level.Generator;
using Level.Player.Data;
using UnityEngine;
using UnityEngine.UI;

public class StarsDisplay : MonoBehaviour {
    [SerializeField] private Sprite[] starsImages;
    [SerializeField] private float animTime;
    private PopUpAnim _popUpAnim;
    private Image _image;
    private bool hasStarted = false;
    private LevelGenerator _levelGenerator;
    private PlayerData _playerData;
    private AudioSource _audioSource;

    private void Awake() {
        _popUpAnim = GetComponent<PopUpAnim>();
        _image = GetComponent<Image>();
        _audioSource = GetComponent<AudioSource>();
    }


    IEnumerator StarsAnimation(int n) {
        _audioSource.pitch = 1.5f;
        for (int i = 0; i <= n; i++) {
            _image.sprite = starsImages[i];
            _popUpAnim.enabled = false;
            _popUpAnim.enabled = true;
            _audioSource.Play();
            yield return new WaitForSeconds(animTime);
            _audioSource.pitch += 0.2f;
            
        }
    }

    private void OnEnable() {
        if (!hasStarted) return;
        print(_levelGenerator);
        print(_playerData);
        print(PersistentDataContainer.PersistentData);
        StartCoroutine(StarsAnimation(PersistentDataContainer.PersistentData.LevelStars(_levelGenerator.World.SilverMoves, _levelGenerator.World.SilverMoves, (int)_playerData.movements)));
    }

    private void Start() {
        hasStarted = true;
        _levelGenerator = FindObjectOfType<LevelGenerator>();
        _playerData = FindObjectOfType<PlayerData>();
    }
}
