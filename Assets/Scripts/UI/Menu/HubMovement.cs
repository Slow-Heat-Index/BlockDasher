using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using  DG.Tweening;
using UnityEngine.Events;

public class HubMovement : MonoBehaviour {
    public UnityEvent OnLevelReached;
    [SerializeField]private List<GameObject> path = new List<GameObject>();
    [SerializeField] private float duration;
    [SerializeField]  int[] checkPointsIndex;
    [SerializeField] private GameObject targetPoints;

    private List<Sequence> sequenceList;
    private int currentLevel;
    private bool initialized = false;

    public void InitEverything() {
        if(initialized) return;
        initialized = true;
        InitPath();
        sequenceList = new List<Sequence>();
        for (int i = 1; i < checkPointsIndex.Length; i++)
        {
            AddSequence(i);
        }
    }
    
    void InitPath() {
        foreach (Transform t in targetPoints.transform) {
            path.Add(t.gameObject);
        }    
    }
    
    [ContextMenu("Next")]
    public void GoNext() {
        sequenceList[currentLevel].PlayForward();
        currentLevel++; 
    }

    [ContextMenu("Previous")]
    public void GoPrevious() {
        currentLevel--;
        sequenceList[currentLevel].PlayBackwards();
        
        
    }
    

    private void AddSequence(int index)
    {
        Sequence sequence = DOTween.Sequence().SetAutoKill(false).Pause().OnPause(OnLevelReached.Invoke);
        for(int i = checkPointsIndex[index - 1]; i <= checkPointsIndex[index]; i++)
        {
            if (i != checkPointsIndex[index - 1])
            {
                var rotation = Vector3.SignedAngle(path[i -1].transform.forward,path[i].transform.position - path[i - 1].transform.position, Vector3.up);
                sequence.Append(transform.DOLocalRotate(new Vector3(0f,rotation,0f), duration));
                
                sequence.Join(transform.DOScaleZ((float) transform.localScale.z * 1.3f,duration*0.5f).SetLoops(2,LoopType.Yoyo));
            }
            sequence.Append(transform.DOLocalMove(path[i].transform.localPosition, duration).SetEase(Ease.InOutCirc));
        }
        sequence.AppendInterval(0.2f);
        sequence.PrependInterval(0.2f);
        sequenceList.Add(sequence);
    }

    public int GetCurrentLevel() {
        return currentLevel;
    }

    public int GetNumLevels() {
        return checkPointsIndex.Length;
    }
}
