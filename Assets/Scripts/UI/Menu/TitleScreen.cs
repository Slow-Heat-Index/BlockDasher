using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class TitleScreen : MonoBehaviour {
    public UnityEvent onGameBegun;
    private RectTransform _rectTransform;
    [SerializeField] private HubWorldAnim hubWorldAnim;
    private ScreensTransitions _screensTransitions;
    [SerializeField] private Button _button;
    

    private void Awake() {
        _rectTransform = GetComponent<RectTransform>();
        _screensTransitions = GetComponent<ScreensTransitions>();
    }

    private void OnEnable() {
        _button.onClick.AddListener(_screensTransitions.ScreenUp);
        _button.onClick.AddListener(() => onGameBegun.Invoke());
    }

    private void OnDisable() {
        _button.onClick.RemoveListener(_screensTransitions.ScreenUp);
        _button.onClick.RemoveListener(_screensTransitions.ScreenUp);
    }
}
