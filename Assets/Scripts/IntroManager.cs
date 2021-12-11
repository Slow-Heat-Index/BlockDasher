using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour {
    [SerializeField] private Image fade;
    [SerializeField] private VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start() {
        var file = System.IO.Path.Combine(Application.streamingAssetsPath, "Video/SlowHeatIndexSmall.mp4");
        videoPlayer.url = file;
        videoPlayer.Play();
        
        videoPlayer.loopPointReached += player => StartCoroutine(OnVideoFinish());
    }

    IEnumerator OnVideoFinish() {
        fade.DOFade(1, 1);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(3);
    }
}