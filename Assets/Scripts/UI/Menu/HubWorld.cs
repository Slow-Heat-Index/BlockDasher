using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubWorld : MonoBehaviour {
    public String worldName;
    public HubWorldAnim hubWorldAnim;
    public HubMovement hubMovement;
    public string[] levels;

    private void Awake() {
        hubWorldAnim = GetComponent<HubWorldAnim>();
    }

    private void OnEnable() {
        hubWorldAnim.OnLevelUp.AddListener(hubMovement.InitEverything);
    }
    
    
}
