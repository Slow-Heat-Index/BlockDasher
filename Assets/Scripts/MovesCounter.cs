using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovesCounter : MonoBehaviour {
    public TextMeshProUGUI DashText;


    private void Awake() {
        DashText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable() {
        DashText.text = "0";
    }

    public void AddMovement(uint move) {
        DashText.text = move.ToString();
    }
}
