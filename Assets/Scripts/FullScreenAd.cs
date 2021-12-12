using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FullScreenAd : MonoBehaviour {
    [SerializeField] private bool skip;
    [SerializeField] private Image progress;
    [SerializeField] private Text secondsLeft;
    [SerializeField] private GameObject adGO;
    [SerializeField] private Button skipButton;
    [SerializeField] private GameObject toActivate;
    private IEnumerator adWait;
    private Tween progressTween;

    public UnityEvent onAdFinished;
    

    private void OnEnable() {
        skipButton.onClick.AddListener(SkipPressed);
    }

    private void OnDisable() {
        skipButton.onClick.RemoveListener(SkipPressed);
    }

    public void PlayAd() {
        adWait = AdDisplay();
        adGO.SetActive(true);
        skipButton.gameObject.SetActive(false);
        StartCoroutine(adWait);

        if (skip) {
            StartCoroutine(SkipAd());
        }
    }

    IEnumerator AdDisplay() {
        progress.fillAmount = 1;
        var timeLeft = 20;
        progressTween = progress.DOFillAmount(0, 20).SetEase(Ease.Linear);
        progressTween.Play();
        
        while (timeLeft > 0) {
            secondsLeft.text = timeLeft.ToString();
            timeLeft--;
            yield return new WaitForSeconds(1);
        }
        
        adGO.SetActive(false);
        if (toActivate != null) {
            toActivate.SetActive(true); 
        }
        
    }

    IEnumerator SkipAd() {
        yield return new WaitForSeconds(5);
        skipButton.gameObject.SetActive(true);
    }

    void SkipPressed() {
        StopCoroutine(adWait);
        progressTween.Restart();
        progressTween.Pause();
        progress.fillAmount = 1;
        adGO.SetActive(false);
        onAdFinished.Invoke();
        
        if (toActivate != null) {
            toActivate.SetActive(true); 
        }
    }
}
