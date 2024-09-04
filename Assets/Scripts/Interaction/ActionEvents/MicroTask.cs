using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MicroTask : MonoBehaviour
{
    public float totalTimeToComplete;

    public float difficultyDecrease; //to determine how much difficulty that will be taken off when completing this task

    public UnityEvent OnTaskComplete;

    public string UID;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //TODO: we want a function that when an object is clicked on will come up with options


    /// <summary>
    /// when the player has completed a task we need to process the end of it
    /// </summary>
    public void ProcessTaskCompletion()
    {
        //decrease the difficulty level
        
        float newDifLevel = SQLiteTest.PullDifficultyLevel(1) + difficultyDecrease;
        
        Debug.LogWarning($"our new diff level is {newDifLevel}");
        
        SQLiteTest.UpdateDifficultyLevel(1, newDifLevel);
        
        DifficultyEventSystem.OnUpdateDifficulty(); //updating after every task is done
        
        OnTaskComplete?.Invoke();
        ActionsEventSystem.OnDeleteFromPersistent(UID);
        //destroy object 
        Destroy(gameObject);
    }
}
