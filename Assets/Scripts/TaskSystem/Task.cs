using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// the purpose of this script is so we can tie physical objects to the tasks and activate
/// certain actions using unity events 
/// </summary>
public class Task : MonoBehaviour
{
    public TaskSO task;

    public UnityEvent StartTask;
    public UnityEvent CompleteTask;

    private void OnEnable()
    {
        task.OnStartTask += ActivateStartTask;
        task.OnCompleteTask += ActivateCompleteTask;
    }

    private void OnDisable()
    {
        task.OnStartTask -= ActivateStartTask;
        task.OnCompleteTask -= ActivateCompleteTask;
    }

    public void ActivateCompleteTask()
    {
        CompleteTask?.Invoke();
    }

    public void ActivateStartTask()
    {
        StartTask?.Invoke();
    }
}
