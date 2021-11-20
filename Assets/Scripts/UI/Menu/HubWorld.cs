using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HubWorld : MonoBehaviour {
    public String worldName;
    public Sprite imageName;
    public HubWorldAnim hubWorldAnim;
    public HubMovement hubMovement;
    public bool isLocked;
    public string[] levels;
    public Vector3[] levelRotation;
    public int starsNeeded;

    private void Awake() {
        hubWorldAnim = GetComponent<HubWorldAnim>();
    }

    private void OnEnable() {
        hubWorldAnim.OnLevelUp.AddListener(hubMovement.InitEverything);
    }

    public void Rotate(int i)
    {
        transform.DORotate(levelRotation[i], 0.8f);
    }
    
    
}
