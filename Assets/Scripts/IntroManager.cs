using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour {
    [SerializeField] private Image fade;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(OnVideoFinish());
    }

    IEnumerator OnVideoFinish() {
        yield return new WaitForSeconds(6.5f);
        fade.DOFade(1, 1);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(3);
    }
}