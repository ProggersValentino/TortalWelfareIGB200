using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// the purpose of this script is to provide a utility for starting and completing tasks
/// </summary>
public class TaskEventManager : MonoBehaviour
{
    public static event UnityAction StartTask;
    public static event UnityAction CompleteTask;
    public static event Func<TaskSO> RecieveCurrentTask;
    public static event UnityAction IncreaseCurrentTaskIndex;

    public static void OnStartTask() => StartTask?.Invoke();
    public static void OnCompleteTask() => CompleteTask?.Invoke();
    public static void OnIncreaseCurrentTaskIndex() => IncreaseCurrentTaskIndex?.Invoke();
    public static TaskSO OnRecieveCurrentTask() => RecieveCurrentTask?.Invoke();
}
