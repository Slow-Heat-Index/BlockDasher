using System.Collections;
using System.Collections.Generic;
using Level.Player.Data;
using UnityEngine;

public class GameplayUI : MonoBehaviour {
    private PlayerData _player;
    
    // Start is called before the first frame update
    void Start() {
        _player = FindObjectOfType<PlayerData>();

        _player.onWin += () => gameObject.SetActive(false);
    }
    
}
