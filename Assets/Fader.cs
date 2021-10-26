using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Fader : MonoBehaviour {
    private Image _image;
    [SerializeField] private float tweenTime;

    private void Awake() {
        _image = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start() {
        _image.enabled = true;
        FadeIn();
    }

    public void FadeIn() {
        _image.DOFade(0, tweenTime);
    }

    public void FadeOut() {
        _image.DOFade(1, tweenTime);
    }
}
