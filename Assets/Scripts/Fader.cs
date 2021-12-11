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
    [SerializeField] private float targetOpacity;

    private void Awake() {
        _image = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start() {
        _image.enabled = true;
        FadeIn();
    }

    public void FadeIn() {
        _image.DOFade(0, tweenTime).SetUpdate(true);
        gameObject.SetActive(false);
    }

    public void FadeOut() {
        _image.DOFade(1, tweenTime).SetUpdate(true);
    }

    public void FadeTo(float opacity) {
        _image.DOFade(opacity, tweenTime).SetUpdate(true);
    }

    public void FadeToInspector() {
        FadeTo(targetOpacity);
    }

    public float GetTweenTime() {
        return tweenTime;
    }
}
