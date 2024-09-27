using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurtleFeederGameMan : MonoBehaviour
{
    public TextMeshProUGUI turtleFedNumberTxt;
    public TextMeshProUGUI turtlesFedResult;

    private int turtlesFed = 0;
    public GameObject endUI;
    

    private void OnEnable()
    {
        TurtleFeederEventSystem.UpdateTurtleFedUI += UpdateTurtlesFed;
        TimerEventManager.TimerCompleted += ProcessEnd;
    }

    private void OnDisable()
    {
        TurtleFeederEventSystem.UpdateTurtleFedUI -= UpdateTurtlesFed;
        TimerEventManager.TimerCompleted -= ProcessEnd;
    }

    // Start is called before the first frame update
    void Start()
    {
        TimerEventManager.OnTimerStart();
        turtleFedNumberTxt.text = turtlesFed.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTurtlesFed()
    {
        turtlesFed++;
        turtleFedNumberTxt.text = turtlesFed.ToString();
    }

    public void ProcessEnd()
    {
        turtlesFedResult.text = turtleFedNumberTxt.text;
        endUI.SetActive(true);
        ChangeTimeScale(0f);
    }
    
    public void ChangeTimeScale(float value)
    {
        Time.timeScale = value;
    }
    
}
