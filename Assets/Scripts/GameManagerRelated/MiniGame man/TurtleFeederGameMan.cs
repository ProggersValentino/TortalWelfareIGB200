using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurtleFeederGameMan : MonoBehaviour
{
    public TextMeshProUGUI turtleFedNumberTxt;
    public TextMeshProUGUI ateRubbishNumberTxt;
    public TextMeshProUGUI turtlesFedResult;
    public TextMeshProUGUI AteRubbishResult;
    public TextMeshProUGUI overallScoreResult;

    private int turtlesFed = 0;
    private int ateRubbish = 0;
    public GameObject endUI;
    

    private void OnEnable()
    {
        TurtleFeederEventSystem.UpdateTurtleFedUI += UpdateTurtlesFed;
        TurtleFeederEventSystem.UpdateAteRubbishUI += UpdateAteRubbish;
        TimerEventManager.TimerCompleted += ProcessEnd;
    }

    private void OnDisable()
    {
        TurtleFeederEventSystem.UpdateTurtleFedUI -= UpdateTurtlesFed;
        TurtleFeederEventSystem.UpdateAteRubbishUI -= UpdateAteRubbish;
        TimerEventManager.TimerCompleted -= ProcessEnd;
    }

    private void Awake()
    {
        TurtleFeederEventSystem.OnGameIsProgressing(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        TimerEventManager.OnTimerStart();
        turtleFedNumberTxt.text = turtlesFed.ToString();
        ateRubbishNumberTxt.text = ateRubbish.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTurtlesFed(int increaseBy)
    {
        turtlesFed += increaseBy;
        if (turtlesFed < 0) return; //so that we dont go into negatives
        turtleFedNumberTxt.text = turtlesFed.ToString();
    }
    
    public void UpdateAteRubbish(int increaseBy)
    {
        ateRubbish += increaseBy;
        ateRubbishNumberTxt.text = ateRubbish.ToString();
    }

    public void ProcessEnd()
    {
        turtlesFedResult.text = turtleFedNumberTxt.text;
        AteRubbishResult.text = ateRubbishNumberTxt.text;
        overallScoreResult.text = (turtlesFed - ateRubbish).ToString();
        endUI.SetActive(true);
        ChangeTimeScale(0f);
    }
    
    public void ChangeTimeScale(float value)
    {
        Time.timeScale = value;
    }
    
}
