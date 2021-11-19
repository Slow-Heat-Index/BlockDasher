using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Level.Player.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ContinueScreen : MonoBehaviour {
    event Action onCountDownOver;
    
    [SerializeField] private Text secondsLeft;
    [SerializeField] private Image progress;
    [SerializeField] GameObject gameplayUiGO;
    [SerializeField] private ScreensTransitions gameOverGO;
    [SerializeField] private Button watchAd;
    private FullScreenAd _ad;
    private Fader _fader;
    private PlayerData _playerData;
    private ScreensTransitions _screensTransitions;

    private void Awake() {
        _screensTransitions = GetComponent<ScreensTransitions>();
        _fader = FindObjectOfType<Fader>();
        _ad = FindObjectOfType<FullScreenAd>();
    }


    private void Start() {
        _playerData = FindObjectOfType<PlayerData>();
        _playerData.onLose += StartCountDown;
        _playerData.onLose += _screensTransitions.ScreenIn;
        onCountDownOver +=_screensTransitions.ScreenUp;
        onCountDownOver += gameOverGO.ScreenIn;
        watchAd.onClick.AddListener(WatchAd);
    }

    void StartCountDown() {
        gameplayUiGO.SetActive(false);
        _fader.FadeTo(0.4f);
        StartCoroutine(CountDown());
    }

    void WatchAd() {
        StopCoroutine(CountDown());
        _screensTransitions.ScreenUp();
        gameOverGO.ScreenIn();
        _ad.PlayAd();
    }

    IEnumerator CountDown() {
        progress.fillAmount = 1;
        var time = 5;
        progress.DOFillAmount(0, 5).SetEase(Ease.Linear);
        while (time > 0) {
            secondsLeft.text = time.ToString();
            time--;
            yield return new WaitForSeconds(1);
        }
        
        onCountDownOver.Invoke();
    }
}
