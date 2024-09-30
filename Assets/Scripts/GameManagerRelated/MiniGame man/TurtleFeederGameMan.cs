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
    // Overall score on screen
    public TextMeshProUGUI overallScoreTxt;

    private int turtlesFed = 0;
    private int ateRubbish = 0;
    // Overall score on screen
    private int overallScore = 0;
    public GameObject endUI;
    public GameObject mainUI;


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
        Debug.LogWarning(SQLiteTest.dbName);
    }

    // Start is called before the first frame update
    void Start()
    {
        endUI.SetActive(false);
        mainUI.SetActive(true);
        TimerEventManager.OnTimerStart();
        turtleFedNumberTxt.text = turtlesFed.ToString();
        ateRubbishNumberTxt.text = ateRubbish.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTotalScore();
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

    public void UpdateTotalScore()
    {
        overallScore = turtlesFed - ateRubbish;
        overallScoreTxt.text = overallScore.ToString();
    }

    public void ProcessEnd()
    {
        turtlesFedResult.text = turtleFedNumberTxt.text;
        AteRubbishResult.text = ateRubbishNumberTxt.text;
        overallScoreResult.text = (turtlesFed - ateRubbish).ToString();
        HowMuchWasEarned(turtlesFed - ateRubbish);
        endUI.SetActive(true);
        mainUI.SetActive(false);
        ChangeTimeScale(0f);
    }

    public void HowMuchWasEarned(int score)
    {
        int moneyEarned = score * 10;
        int newMoneyValue = 0;
        
        
        if(moneyEarned > 0)
        {
            newMoneyValue = moneyEarned + SQLiteTest.PullPlayersMoney(1);
        }
        
        SQLiteTest.UpdatePlayersMoney(1, newMoneyValue);
    }
    
    public void ChangeTimeScale(float value)
    {
        Time.timeScale = value;
    }
    
}
