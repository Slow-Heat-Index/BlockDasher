using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using  DG.Tweening;

public class HubMovement : MonoBehaviour
{
    [SerializeField]private List<GameObject> path = new List<GameObject>();
    [SerializeField] private float duration;
    [SerializeField]  int[] checkPointsIndex;
    [SerializeField] private GameObject targetPoints;
    
    private List<Sequence> sequenceList;
    private int currentLevel;

    public void InitEverything() {
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
    public void GoNext()
    {
        sequenceList[currentLevel].PlayForward();
        currentLevel++; 
    }

    [ContextMenu("Previous")]
    public void GoPrevious()
    {
        currentLevel--;
        sequenceList[currentLevel].PlayBackwards();
    }
    

    private void AddSequence(int index)
    {
        Sequence sequence = DOTween.Sequence().SetAutoKill(false).Pause();
        for(int i = checkPointsIndex[index - 1]; i <= checkPointsIndex[index]; i++)
        {
            if (i != checkPointsIndex[index - 1])
            {
                sequence.Append(transform.DOLookAt(path[i].transform.position, duration));
                sequence.Join(transform.DOScaleZ((float) transform.localScale.z * 1.15f,duration*0.5f).SetLoops(2,LoopType.Yoyo));
            }
            sequence.Append(transform.DOMove(path[i].transform.position, duration).SetEase(Ease.InOutCirc));
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
