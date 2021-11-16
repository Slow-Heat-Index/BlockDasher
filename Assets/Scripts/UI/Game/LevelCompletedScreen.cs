using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Level.Generator;
using Level.Player.Data;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ScreensTransitions), typeof(RectTransform))]
public class LevelCompletedScreen : MonoBehaviour {
    private PlayerData _player;
    private ScreensTransitions _screensTransitions;
    private RectTransform _rectTransform;
    [SerializeField] private Fader _background;

    [SerializeField] private TextMeshProUGUI _steps;
    [SerializeField] private TextMeshProUGUI _time;


    private void Start() {
        _player = FindObjectOfType<PlayerData>();
        _screensTransitions = GetComponent<ScreensTransitions>();
        _rectTransform = GetComponent<RectTransform>();

        _player.onWin += _screensTransitions.ScreenIn;
        _player.onWin += () => _steps.text = $"Steps: {_player.movements}";
        _player.onWin += () => {
            _background.gameObject.SetActive(true);
            _background.FadeTo(0.4f);
        };
            
        _rectTransform.anchoredPosition = new Vector2(0, -_rectTransform.rect.height);
    }
    
    


}
