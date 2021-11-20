using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadTwitter : MonoBehaviour {
    private Button _button;

    private void Awake() {
        _button = GetComponent<Button>();
    }

    private void OnEnable() {
        _button.onClick.AddListener(OpenTwitter);
    }
    
    private void OnDisable() {
        _button.onClick.RemoveListener(OpenTwitter);
    }

    void OpenTwitter() {
        Application.OpenURL("https://twitter.com/SlowHeatIndex");
    }
}
