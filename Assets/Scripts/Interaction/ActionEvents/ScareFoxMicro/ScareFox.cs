using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareFox : MicroTask
{
    public Camera mainCam;

    public GameObject microgameUI; //this is the gameobject of the micro game

    public GameObject torchLight; //the light to find the fox

    private void OnEnable()
    {
        TimerEventManager.TimerCompleted += EndMG;
    }

    private void OnDisable()
    {
        TimerEventManager.TimerCompleted -= EndMG;
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        mainCam = FindObjectOfType<Camera>();    
        microgameUI.transform.position = mainCam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 12));

        // microgameUI.transform.position = new Vector3(Screen.width / 2, 5, Screen.height / 2);
    }

    // Update is called once per frame
    void Update()
    {
        torchLight.transform.position = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, 12));

    }

    public void StartMG()
    {
        microgameUI.SetActive(true);
    }

    public void EndMG()
    {
        ProcessTaskCompletion(taskData._difficultyDecreaseLevel);
        Destroy(gameObject);
    }
}
