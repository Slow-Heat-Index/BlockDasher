using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraTransitions : MonoBehaviour {
    [SerializeField] private Transform _target;
    [SerializeField] private float _transitionTime;
    private Vector3 _initialpos;
    private Vector3 _initialrot;

    private void Awake() {
        _initialpos = transform.position;
        _initialrot = transform.rotation.eulerAngles;
    }

    public void MoveTo() {
        transform.DOMove(_target.position, _transitionTime);
        transform.DORotate(_target.rotation.eulerAngles, _transitionTime);
    }

    public void ResetCamPos() {
        transform.DOMove(_initialpos, _transitionTime);
        transform.DORotate(_initialrot, _transitionTime);
    }
}
