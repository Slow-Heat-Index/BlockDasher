using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(TextMeshProUGUI))]
public class CoinsVisor : MonoBehaviour {
    private TextMeshProUGUI _textMeshProUGUI;

    private void Awake() {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(InitCoins());
    }

    IEnumerator InitCoins() {
        yield return new WaitForEndOfFrame();
        _textMeshProUGUI.text = PersistentDataContainer.PersistentData.coins.ToString();
    }

    private void Update() {
        _textMeshProUGUI.text = PersistentDataContainer.PersistentData.coins.ToString();
    }
}
