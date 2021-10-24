using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubWorld : MonoBehaviour {
    public String worldName;
    public HubWorldAnim hubWorldAnim;

    private void Awake() {
        hubWorldAnim = GetComponent<HubWorldAnim>();
    }
    
}
