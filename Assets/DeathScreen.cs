using System;
using System.Collections;
using System.Collections.Generic;
using Level.Player.Data;
using UnityEngine;

public class DeathScreen : MonoBehaviour {
    private PlayerData _playerData;
    void Start() {
        _playerData = FindObjectOfType<PlayerData>();
        _playerData.onLose += () => gameObject.SetActive(true);
        
        GetComponent<RectTransform>().localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }

}
