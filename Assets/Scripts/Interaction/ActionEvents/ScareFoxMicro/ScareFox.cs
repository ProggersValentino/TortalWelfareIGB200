using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class ScareFox : MicroTask
{
    public Camera mainCam;

    public GameObject microgameUI; //this is the gameobject of the micro game

    public GameObject torchLight; //the light to find the fox

    //switching fox related
    public GameObject currentFox;
    public List<GameObject> possibleFoxAreas = new List<GameObject>();

    public float timer;
    public float timeToSwitch;
    
    private void OnEnable()
    {
        TimerEventManager.TimerCompleted += EndMG;
        SceneManager.sceneLoaded += InitMinigameWhenSceneLoaded;
        
        
    }

    private void OnDisable()
    {
        TimerEventManager.TimerCompleted -= EndMG;
        SceneManager.sceneLoaded -= InitMinigameWhenSceneLoaded;

        mainCam = null;
    }
    
    
    public void InitMinigameWhenSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.LogWarning($"scene is laoded heeeeee {scene.name}");
        // mainCam = FindObjectOfType<Camera>();    
        // microgameUI.transform.position = mainCam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 12));

    }
    
    // Start is called before the first frame update
    void Start()
    {
        // mainCam = FindObjectOfType<Camera>();    
        // microgameUI.transform.position = mainCam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 12));
        //
        // microgameUI.transform.position = new Vector3(Screen.width / 2, 5, Screen.height / 2);
        
        MoveFox();
    }

    // Update is called once per frame
    void Update()
    {
        if(microgameUI.activeInHierarchy) torchLight.transform.position = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, 12));

        if (Time.time > timer && microgameUI.activeInHierarchy)
        {
            timer = timeToSwitch + Time.time;
            currentFox.SetActive(false);
            
            MoveFox();
        }

    }

    public void StartMG()
    {
        mainCam = FindObjectOfType<Camera>();    
        microgameUI.transform.position = mainCam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 12));

        
        microgameUI.SetActive(true);
    }

    public void EndMG()
    {
        if (!microgameUI.activeInHierarchy) return;
        
        ProcessTaskCompletion(taskData._difficultyDecreaseLevel);
        Destroy(gameObject);
    }

    public void MoveFox()
    {
        int foxPos = Random.Range(0, possibleFoxAreas.Count - 1);
        
        currentFox = possibleFoxAreas[foxPos];

        currentFox.SetActive(true);

    }
    
}
