using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class FlickerTween : MonoBehaviour {

    [SerializeField] private float _minAlpha;
    [SerializeField] private float _maxAlpha;
    [SerializeField] private float _tweenTime;
    
    private Action _flicker;
    private Image _img;
    private TextMeshProUGUI _txt;
    
    private void Awake() {
        _flicker = CheckObject();
    }

    // Update is called once per frame
    private void Start() {
        _flicker.Invoke();
    }

    private Action CheckObject() {
        if (TryGetComponent(out TextMeshProUGUI tmp)) {
            _txt = tmp;
            return TextUpdate;
        }

        if (TryGetComponent(out Image img)) {
            _img = img;
            return ImageUpdate;
        }

        return null;
    }


    void TextUpdate() {
        _txt.DOFade(_minAlpha, _tweenTime).SetLoops(-1, LoopType.Yoyo);
    }

    void ImageUpdate() {
        _img.DOFade(_minAlpha, _tweenTime).SetLoops(-1, LoopType.Yoyo);
    }
    
}
