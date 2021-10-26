using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour {
    private VideoPlayer _videoPlayer;
    [SerializeField] private Image _fadeToBlack;
    [SerializeField] private VideoClip _videoClip;

    private void Awake() {
        _videoPlayer = GetComponent<VideoPlayer>();
        _videoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,"SlowHeatIndex.mp4");
    }

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(WaitVideoToFinish());
    }

    IEnumerator WaitVideoToFinish() {
        while (_videoPlayer.isPrepared) {
            yield return null;
        }
        yield return new WaitForSeconds(6);
        _fadeToBlack.DOFade(1, 1);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }
}
