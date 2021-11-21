using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PopUpAnim : MonoBehaviour {
    [SerializeField] private float tweenTime;
    [SerializeField] private float amplitude = 1;
    [SerializeField] private float period = 0;
    private Vector2 target;

    private void Awake() {
        target = transform.localScale;
        
    }

    private void OnEnable() {
        PopUp();
    }


    public void PopUp() {
        transform.localScale = Vector3.zero;
        transform.DOScale(target, tweenTime).SetEase(Ease.OutElastic, amplitude, period).SetUpdate(true);
    }
    
    public float GetTweenTime() {
        return tweenTime;
    }


}
