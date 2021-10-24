using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSceneLoader : MonoBehaviour {

    [SerializeField] private int _sceneToLoadId;
    private Button _button;


    private void Awake() {
        _button = GetComponent<Button>();
    }

    private void OnEnable() {
        _button.onClick.AddListener(LoadScene);
    }

    private void OnDisable() {
        _button.onClick.RemoveListener(LoadScene);
    }

    void LoadScene() {
        SceneManager.LoadScene(_sceneToLoadId);
    }
}
