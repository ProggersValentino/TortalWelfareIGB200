using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the purpose of this script is to progress through the tasks
/// with the added utility of being able to apply this script to Unity events
/// 
/// </summary>
public class TaskProgressor : MonoBehaviour
{
    public void CompleteTask()
    {
        TaskEventManager.OnCompleteTask();
    }

    public void IncrementIndex()
    {
        TaskEventManager.OnIncreaseCurrentTaskIndex();
    }
}
