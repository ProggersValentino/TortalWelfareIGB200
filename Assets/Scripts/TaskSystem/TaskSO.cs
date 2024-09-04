using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "DataBank/New Task")]
public class TaskSO : ScriptableObject
{
    [TextArea(3, 10)]
    [SerializeField] private string taskDescription;
    
    public string _taskDescription
    {
        get { return taskDescription; }
    }

    [SerializeField] private DialogueSO conversation;
    
    public DialogueSO _conversation
    {
        get { return conversation; }
    }

    [SerializeField] private bool isComplete;
    
    public bool _isComplete
    {
        get { return isComplete; }
        set { isComplete = value; }
    }

    public void CompleteTask()
    {
        isComplete = true;
        OnCompleteTask?.Invoke();
    }

    public UnityAction OnCompleteTask;
    public UnityAction OnStartTask;

    public void StartTask() => OnStartTask?.Invoke();
    

}
