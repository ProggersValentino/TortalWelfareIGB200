using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public List<TaskSO> tasks = new List<TaskSO>();

    public int currentTask = 0;

    private void OnEnable()
    {
        TaskEventManager.StartTask += InitTask;
        TaskEventManager.CompleteTask += CompleteCurrentTask;
        TaskEventManager.RecieveCurrentTask += CurrentTask;
        TaskEventManager.IncreaseCurrentTaskIndex += IncrementCurrentTask;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //when called it will give back a task
    public TaskSO CurrentTask()
    {
        return tasks[currentTask];
    }
    
    /// <summary>
    /// when we want initialize the first the task
    /// </summary>
    public void InitFirstTask()
    {
        tasks[currentTask].StartTask();
        if(tasks[currentTask]._conversation != null) DialogueEventSystem.OnStartDialogue(tasks[currentTask]._conversation._dialogueChain);
    }

    public void InitTask()
    {
        tasks[currentTask].StartTask();
        StartDialogue();
    }

    public void StartDialogue()
    {
        if(tasks[currentTask]._conversation != null) DialogueEventSystem.OnStartDialogue(tasks[currentTask]._conversation._dialogueChain);
    }

    public void CompleteCurrentTask()
    {
        tasks[currentTask].CompleteTask();
        IncrementCurrentTask();
        Debug.LogWarning($"can we move onto the next task {CanWeMoveToNextTask()}");
        if(CanWeMoveToNextTask()) InitTask();
    }

    public void IncrementCurrentTask()
    {
        currentTask++;
    }
    
    public bool CanWeMoveToNextTask()
    {
        if (currentTask <= tasks.Count - 1) return true;

        return false;
    }
}
