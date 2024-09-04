using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public Image difficultyBarUI;
    
    public GameObject microTaskPref;

    public List<GameObject> thingsToSpawn = new List<GameObject>();

    public List<GameObject> spawnedInThings = new List<GameObject>();
    
    public GameObject spawnStart;
    public GameObject spawnFinish;
    
    private void OnEnable()
    {
        ActionsEventSystem.SendReadySignal += ReadyMicroTasks;
    }

    private void OnDisable()
    {
        ActionsEventSystem.SendReadySignal -= ReadyMicroTasks;
    }

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        // MultipleSceneManager.SetActiveScene("RangerPerspective");  
        // thingsToSpawn = ActionsEventSystem.OnRetrieveMicroTasks();
        // SummonMicroTasks(thingsToSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    /// <summary>
    /// 
    /// </summary>
    public void SummonMicroTasks(List<GameObject> microTasks)
    {
        foreach (GameObject microTask in microTasks)
        {
            Vector3 newRanPos = new Vector3(
                Random.Range(spawnStart.transform.position.x, spawnFinish.transform.position.x),
                0.05f,
                Random.Range(spawnStart.transform.position.z, spawnFinish.transform.position.z)); 
            
            GameObject newMicroTask = Instantiate(microTask, newRanPos, microTask.transform.rotation);

            //Undo.MoveGameObjectToScene(newMicroTask, SceneManager.GetSceneByName("RangerPerspective"), "yes");
            newMicroTask.transform.SetParent(transform, true); //to prevent it from running away to the other active scenes (pain)
        
            MicroTask summonedMTask = newMicroTask.GetComponent<MicroTask>();
            
            spawnedInThings.Add(microTask);
            
            //summonedMTask.ShiftDifficultyLevel += difficultyLevel.CalculateDifference;    
        }
        
    }

    public void ReadyMicroTasks()
    {
        thingsToSpawn = ActionsEventSystem.OnRetrieveMicroTasks();
        
        SummonMicroTasks(thingsToSpawn);
    }
    
    
    
}
