using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBank/New Micro Task")]
public class MicroTaskSO : ScriptableObject
{
    [SerializeField] private Sprite taskSprite;
    
    public Sprite _taskSprite
    {
        get { return taskSprite; }
    }

    [SerializeField] private float difficultyDecreaseLevel; //to determine how much difficulty that will be taken off when completing this task
    
    public float _difficultyDecreaseLevel
    {
        get { return difficultyDecreaseLevel; }
    }


    [SerializeField] private float timeToCompleteTask;
    
    public float _timeToCompleteTask
    {
        get { return timeToCompleteTask; }
    }

}
