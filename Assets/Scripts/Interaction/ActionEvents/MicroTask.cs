using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MicroTask : MonoBehaviour
{
    private SpriteRenderer taskSprite;
    public SpriteRenderer _taskSprite
    {
        get { return taskSprite; }
    }

    private bool timeRunning = false;
    private float timeTillFinished;
    public Image barImg;
    
    public float totalTimeToComplete;

    public MicroTaskSO taskData;

    //public float difficultyDecrease; 

    public UnityEvent OnTaskComplete;

    public string UID;

    private void Awake()
    {
        taskSprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        taskSprite.sprite = taskData._taskSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator StartProcessingTask(float timeAmount)
    {
        float time = Time.deltaTime;

        timeTillFinished = timeAmount + Time.deltaTime;


        while (barImg.fillAmount < 0.96)
        {

            barImg.fillAmount += Time.deltaTime * timeAmount; 
            
            yield return new WaitForSeconds(Time.deltaTime);
        }
        
        
        AudioEventSystem.OnPlayAudio("Confirm_Press");
        ProcessTaskCompletion(taskData._difficultyDecreaseLevel);
    }
    

    /// <summary>
    /// when the player has completed a task we need to process the end of it
    /// </summary>
    public void ProcessTaskCompletion(float difficultyLevel)
    {
        //decrease the difficulty level
        
        float newDifLevel = SQLiteTest.PullDifficultyLevel(1) + difficultyLevel;
        
        Debug.LogWarning($"our new diff level is {newDifLevel}");
        
        SQLiteTest.UpdateDifficultyLevel(1, newDifLevel);
        
        DifficultyEventSystem.OnUpdateDifficulty(); //updating after every task is done
        
        OnTaskComplete?.Invoke();
        
        //AudioEventSystem.OnPlayAudio("foxSoundSFX");
        ActionsEventSystem.OnIsCompletingTask(false); //player has completed the task
        ActionsEventSystem.OnDeleteFromPersistent(UID);
        //destroy object 
        Destroy(gameObject);
    }
}
