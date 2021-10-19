using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HubWorldAnim : MonoBehaviour {
    [SerializeField] private float _tweenTime;
    
    public void PopUp() {
        transform.DOMove(Vector3.zero, _tweenTime);
        transform.DORotate(new Vector3(0, 180, 0), 0.5f, RotateMode.Fast)
            .SetEase(Ease.Linear)
            .OnComplete(Idle);
    }

    void Idle() {
        
    }
}
