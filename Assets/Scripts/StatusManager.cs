using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour {
    [SerializeField] private Image _imageWater;
    [SerializeField] private Image _imageSand;
    [SerializeField] private Sprite[] spritesSand;
    [SerializeField] private Sprite[] spritesWater;


    public void SetSand(int i) {
        _imageSand.color = Color.white;
        _imageSand.sprite = spritesSand[i];
    }
    
    public void SetWater(int i) {
        _imageWater.color = Color.white;
        _imageWater.sprite = spritesWater[i];
    }

    public void ResetWater() {
        _imageWater.color = new Color(0, 0, 0, 0);
    }

    public void ResetSand() {
        _imageSand.color = new Color(0, 0, 0, 0);
    }
}
