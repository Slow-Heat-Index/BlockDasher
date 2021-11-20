using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubWorld : MonoBehaviour {
    public String worldName;
    public Sprite imageName;
    public HubWorldAnim hubWorldAnim;
    public HubMovement hubMovement;
    public bool isLocked;
    public string[] levels;
    public bool[] lockedLevels;

    private void Awake() {
        hubWorldAnim = GetComponent<HubWorldAnim>();
    }

    private void OnEnable() {
        hubWorldAnim.OnLevelUp.AddListener(hubMovement.InitEverything);
    }
    
    
}
