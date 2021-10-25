using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  DG.Tweening;

public class HubMovement : MonoBehaviour
{
    [SerializeField]private Vector3[] path;
    [SerializeField] private float duration;
    [SerializeField]private int[] checkPointsIndex;
    private List<Sequence> sequenceList;
    private int currentLevel;
    //private Tweener pathing;

    void Start()
    {
        sequenceList = new List<Sequence>();
        for (int i = 1; i < checkPointsIndex.Length; i++)
        {
            AddSequence(i);
        }
        
        //pathing = transform.DOPath(path, duration).SetAutoKill(false).SetEase(Ease.InOutBack).SetDelay(0.5f);
        //pathing.Pause();

        


    }
    [ContextMenu("Next")]
    void GoNext()
    {
        sequenceList[currentLevel].PlayForward();
        currentLevel++; 
    }

    [ContextMenu("Previous")]
    void GoPrevious()
    {
        currentLevel--;
        sequenceList[currentLevel].PlayBackwards();
    }
    

    private void AddSequence(int index)
    {
        Sequence sequence = DOTween.Sequence().SetAutoKill(false).Pause();
        for(int i = checkPointsIndex[index - 1]; i <= checkPointsIndex[index]; i++)
        {
            sequence.Append(transform.DOMove(path[i], duration).SetEase(Ease.InOutCirc));
        }
        sequence.AppendInterval(0.2f);
        sequence.PrependInterval(0.2f);
        sequenceList.Add(sequence);
    }
}
