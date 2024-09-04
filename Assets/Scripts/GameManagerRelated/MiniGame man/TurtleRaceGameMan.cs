using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class TurtleRaceGameMan : MonoBehaviour
{
    public GameObject obstaclePref;
   
    //racer related
    public GameObject racer;
    public Transform goal;
    
    public List<Transform> startPos;
        
    public int obstacleCount;

    public List<GameObject> racers;  

    [Range(0, 3)] public float multiplierEnhancer = 1;

    public Transform startSpawnPoint;
    public Transform endSpawmPoint;

    public RaceTimer RT;
    public Finishline FL;
    
    //results related
    public GameObject endUI;
    public TextMeshProUGUI timerResult;
    public TextMeshProUGUI placeResult;

    //private SQLiteTest yes;
    
    private void OnEnable()
    {
        FL.processEnd.AddListener(ProcessEndOfMinigame);
        SceneManager.sceneLoaded += InitMinigameWhenSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= InitMinigameWhenSceneLoaded;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
        //Debug.LogWarning(playerPlacement);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitMinigameWhenSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.LogWarning($"scene is laoded heeeeee {scene.name}");
        
        SummonObstacles(obstaclePref);
        SummonRacers(racer);
        TimerEventManager.OnTimerStart();
        
        
        //yes = racer.AddComponent<SQLiteTest>();

        //SQLiteTest.pullFromDataBase("SELECT * FROM Stocks");
    }

    /// <summary>
    /// determines how tricky the area will be to navigate
    /// </summary>
    /// <returns></returns>
    public int DetermineObstacleCount()
    {
        //we pull right from the database the difficulty value 
        Debug.LogWarning(SQLiteTest.PullDifficultyLevel(1));
        return (int)((obstacleCount * SQLiteTest.PullDifficultyLevel(1)) * multiplierEnhancer) / 10; //divide by 10 to make it not so crazy
    }
    
    /// <summary>
    /// spawn the obstacles
    /// </summary>
    /// <param name="obstaclePref"></param>
    public void SummonObstacles(GameObject obstaclePref)
    {
        int amountToSummon = DetermineObstacleCount();
        
        Debug.Log("yes we are spawn obs");
        
        for (int i = 0; i < amountToSummon; i++)
        {
            Vector3 randomPoint = new Vector3(Random.Range(startSpawnPoint.position.x, endSpawmPoint.position.x),
                0.2f,
                Random.Range(startSpawnPoint.position.z, endSpawmPoint.position.z));

            GameObject ob = Instantiate(obstaclePref, randomPoint, obstaclePref.transform.rotation);
            
            ob.transform.SetParent(transform, true);
        }
    }

    public void SummonRacers(GameObject racerPref)
    {
        Debug.Log("yes we are spawn racers");
        
        foreach (Transform startingPos in startPos)
        {
            GameObject racer = Instantiate(racerPref, startingPos.transform.position, racerPref.transform.rotation);

            racer.transform.SetParent(transform, true);
            
            RacerAI AI = racer.GetComponent<RacerAI>();

            AI.SetAIDestination(goal);
        }
    }
    
    
    /// <summary>
    /// when the player crosses the finishline we want to process the end of the game
    /// like score and rewards etc
    /// </summary>
    public void ProcessEndOfMinigame()
    {
        TimerEventManager.OnTimerStop();
        timerResult.text = RT.timerText.text;
        placeResult.text = FL.FindPlayerPlacement().ToString();
        endUI.SetActive(true);
        ChangeTimeScale(0f);
        
        //TODO: do a reward system that gives the player a reward and adds it to inventory
    }

    public void ChangeTimeScale(float value)
    {
        Time.timeScale = value;
    }
}
