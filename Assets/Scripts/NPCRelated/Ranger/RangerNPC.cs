using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangerNPC : MonoBehaviour, IInteractable
{
    public RangerData rangerData;

    private NavMeshAgent npcBrain;

    private MicroTask currentTask;
    
    public Camera cam;

    private void OnEnable()
    {
        npcBrain = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(cam.transform.position);
    }

    public RangerNPC SetRangerAsSelected()
    {
        return this;
    }
    
    public bool OnInteract(bool isTrue)
    {
        Debug.Log($"NPC Gaming {isTrue}");
        return isTrue;
    }
    
    //set target which takes in a microtask
    public void CompleteTask(MicroTask selectedTask)
    {
        currentTask = selectedTask;
        npcBrain.SetDestination(selectedTask.transform.position);

        StartCoroutine(DoTask());
        
        Debug.LogWarning(npcBrain.isStopped); 
    }

    /// <summary>
    /// we have clicked on a valid task and are doing it now
    /// </summary>
    /// <returns></returns>
    IEnumerator DoTask()
    {
        while (!AtDestination(currentTask.transform.position, transform.position))
        {
            Debug.LogWarning("we going");
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(currentTask.totalTimeToComplete); //time it takes to do task
        
        currentTask.ProcessTaskCompletion(); //completing the task
        StopCoroutine(DoTask());
    }

    /// <summary>
    /// are we at the target destination set
    /// </summary>
    /// <param name="target"></param>
    /// <param name="currentPos"></param>
    /// <returns></returns>
    bool AtDestination(Vector3 target, Vector3 currentPos)
    {
        return Vector3.Distance(target, currentPos) <= (npcBrain.stoppingDistance - 0.5f);
    }
    
}

[Serializable]
public class RangerData
{
    public Vector3 position;
    public RangerSO rangerSO;
}
