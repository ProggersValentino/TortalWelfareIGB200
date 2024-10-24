using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaceTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    public float amountOfTime;
    
    public enum TimerType {countdown, stopwatch}

    public TimerType timerType;

    public float timerToDisplay;

    private bool isRunning;

    // Bar Fill
    public Image timeBar;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        TimerEventManager.TimerStart += TimerManagerOnTimerStart;
        TimerEventManager.TimerStop += TimerManagerOnTimerStop;
        
        TimerEventManager.TimerUpdate += TimerManagerOnTimerUpdate;
        TimerEventManager.TimerReset += TimerManagerOnTimerReset;
        TimerEventManager.TimerReset += TimerManagerOnTimerUIReset;
    }

    private void OnDisable()
    {
        TimerEventManager.TimerStart -= TimerManagerOnTimerStart;
        TimerEventManager.TimerStop -= TimerManagerOnTimerStop;
        
        TimerEventManager.TimerUpdate -= TimerManagerOnTimerUpdate;
        TimerEventManager.TimerReset -= TimerManagerOnTimerReset;
        TimerEventManager.TimerReset -= TimerManagerOnTimerUIReset;
    }

    private void TimerManagerOnTimerUpdate(float value)
    {
        timerToDisplay += value;
    }

    public void TimerManagerOnTimerReset()
    {
        timerToDisplay = amountOfTime;
    }

    public void TimerManagerOnTimerUIReset()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timerToDisplay);
        timerText.text = timeSpan.ToString(@"ss");
    }
    
    private void TimerManagerOnTimerStop()
    {
        isRunning = false;
    }
    
    private void TimerManagerOnTimerStart(TimerType typeOfTimer)
    {
        if(typeOfTimer == timerType) isRunning = true;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void timeBarUpdate()
    {
        timeBar.fillAmount = timerToDisplay / amountOfTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning) return;
        if (timerType == TimerType.countdown && timerToDisplay < 0.0f)
        {
            TimerEventManager.OnTimerStop();
            TimerEventManager.OnTimerComplete();
            return;
        }

        timerToDisplay += timerType == TimerType.countdown ? -Time.deltaTime : Time.deltaTime;

        if(timeBar)
        {
            timeBar.color = Color.Lerp(Color.red, Color.green, timeBar.fillAmount);
            timeBarUpdate();
        }

        TimeSpan timeSpan = TimeSpan.FromSeconds(timerToDisplay);
        if(timerType == TimerType.stopwatch) timerText.text = timeSpan.ToString(@"mm\:ss\:ff");
        else timerText.text = timeSpan.ToString(@"ss");
    }
}
