using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Level.Cameras.Controller;
using Level.Generator;
using Level.Player.Controller;
using Level.Player.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ContinueScreen : MonoBehaviour {
    public event Action onCountDownOver;
    public GameObject winGO;
    public Button watchAd;
    
    [SerializeField] private Text secondsLeft;
    [SerializeField] private Image progress;
    [SerializeField] GameObject gameplayUiGO;
    private FullScreenAd _ad;
    

     private void Start() {
         _ad = FindObjectOfType<FullScreenAd>();
        
         gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
         gameObject.SetActive(false);
     }


     public void StartCountDown() {
        
        gameplayUiGO.SetActive(false);
        StartCoroutine(CountDown());
    }

    public void WatchAd() {
        StopCoroutine(CountDown());
        gameObject.SetActive(false);
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
        
        gameObject.SetActive(false);
        onCountDownOver.Invoke();
    }
}
