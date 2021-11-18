using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using DG.Tweening;
using Level.Generator;
using Level.Player.Data;
using Sources;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ScreensTransitions), typeof(RectTransform))]
public class LevelCompletedScreen : MonoBehaviour {
    private PlayerData _player;
    private ScreensTransitions _screensTransitions;
    private RectTransform _rectTransform;

    [SerializeField] private TextMeshProUGUI _steps;
    [SerializeField] private TextMeshProUGUI _time;


    private void Start() {
        _player = FindObjectOfType<PlayerData>();
        _screensTransitions = GetComponent<ScreensTransitions>();
        _rectTransform = GetComponent<RectTransform>();

        _player.onWin += _screensTransitions.ScreenIn;
        _player.onWin += () => _steps.text = $"Steps: {_player.movements}";
        _player.onWin += () =>
        {
            PersistentDataContainer.PersistentData.AddLevelCompleted(LevelData.LevelToLoad.Path,(int) _player.movements,1);
            DataAccess.Save(PersistentDataContainer.PersistentData);
        };

        _rectTransform.anchoredPosition = new Vector2(0, -_rectTransform.rect.height);
    }
    
    


}
