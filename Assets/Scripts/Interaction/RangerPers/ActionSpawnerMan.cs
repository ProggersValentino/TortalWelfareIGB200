using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ActionSpawnerMan : MonoBehaviour
{

    public int numberOfMicroTasksAllowed;
    
    public GameObject normalAction;
    
    //TODO: add a SO data for different types of normal actions to change the sprites 
    //public List<GameObject> ActionsToSpawn = new List<GameObject>();

    public Dictionary<string, GameObject> actionsToSpawn = new Dictionary<string, GameObject>();

    private string currentActionIndex = "";

    public List<GameObject> test = new List<GameObject>();
    public List<string> testString = new List<string>();

    private void OnEnable()
    {
        ActionsEventSystem.RetrieveMicroTasks += ReturnListOfActions;
        ActionsEventSystem.DeleteMicroTaskFromPersistent += RemoveAction;
        ActionsEventSystem.InitiateInjection += InjectNewActionToSpawn;
        
        InjectNewActionToSpawn(2);
        test = actionsToSpawn.Values.ToList();
        testString = actionsToSpawn.Keys.ToList();
    }

    private void OnDisable()
    {
        ActionsEventSystem.RetrieveMicroTasks -= ReturnListOfActions;
        ActionsEventSystem.DeleteMicroTaskFromPersistent -= RemoveAction;
        ActionsEventSystem.InitiateInjection -= InjectNewActionToSpawn;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InjectNewActionToSpawn(int injectAmount)
    {
        if (actionsToSpawn.Count > numberOfMicroTasksAllowed) return;        
        for (int i = 0; i < injectAmount; i++)
        {
            currentActionIndex = GenerateRandomUID();
            actionsToSpawn.TryAdd(currentActionIndex, normalAction);
            actionsToSpawn.TryGetValue(currentActionIndex, out GameObject go);

            MicroTask newMT = go.GetComponent<MicroTask>();
            newMT.UID = currentActionIndex;
            
            //add a way to randomise the sprite and how much difficulty it reduces
        }
        
        ActionsEventSystem.OnSendReadySignal();
    }

    public List<GameObject> ReturnListOfActions()
    {
        return actionsToSpawn.Values.ToList();
    }
    
    /// <summary>
    /// remove the action from the queue
    /// </summary>
    /// <param name="searchKey"></param>
    public void RemoveAction(string searchKey)
    {
        actionsToSpawn.Remove(searchKey);
    }

    /// <summary>
    /// generates a random UID for the microtask dictionary key
    /// </summary>
    /// <returns></returns>
    public string GenerateRandomUID()
    {
        string UID = GenerateRandomLetters(5);

        int UValue = Random.Range(0, Int32.MaxValue);

        UID += UValue.ToString();

        return UID;
    }

    public string GenerateRandomLetters(int NoOfLetters)
    {
        string endResult = "";
        for (int i = 0; i < NoOfLetters; i++)
        {
            int charValue = Random.Range(97, 122); //the ASICC table indexes for lowercase letters

            char letter = Convert.ToChar(charValue);

            endResult += letter;

        }

        return endResult;
    }
}
