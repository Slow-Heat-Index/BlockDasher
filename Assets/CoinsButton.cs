using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.UI;

public class CoinsButton : MonoBehaviour {
    [SerializeField] private int value;
    [SerializeField] private bool onlyOnce;
    private Button _button;
    private void Awake() {
        _button = GetComponent<Button>();
    }

    private void OnEnable() {
        _button.onClick.AddListener(Pressed);
    }
    
    private void OnDisable() {
        _button.onClick.RemoveListener(Pressed);
    }

    void Pressed() {
        PersistentDataContainer.PersistentData.ModifyCoins(value);
        if (onlyOnce) {
            _button.enabled = false;
        }
    }
}
