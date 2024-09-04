using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    
    public enum TimerType {countdown, stopwatch}

    public TimerType timerType;

    public float timerToDisplay;

    private bool isRunning;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        TimerEventManager.TimerStart += TimerManagerOnTimerStart;
        TimerEventManager.TimerStop += TimerManagerOnTimerStop;
        
        TimerEventManager.TimerUpdate += TimerManagerOnTimerUpdate;
    }

    private void OnDisable()
    {
        TimerEventManager.TimerStart -= TimerManagerOnTimerStart;
        TimerEventManager.TimerStop -= TimerManagerOnTimerStop;
        
        TimerEventManager.TimerUpdate -= TimerManagerOnTimerUpdate;
    }

    private void TimerManagerOnTimerUpdate(float value)
    {
        timerToDisplay += value;
    }

    private void TimerManagerOnTimerStop()
    {
        isRunning = false;
    }

    private void TimerManagerOnTimerStart()
    {
        isRunning = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning) return;
        if (timerType == TimerType.countdown && timerToDisplay < 0.0f)
        {
            TimerEventManager.OnTimerStop();
            return;
        }

        timerToDisplay += timerType == TimerType.countdown ? -Time.deltaTime : Time.deltaTime;
        
        TimeSpan timeSpan = TimeSpan.FromSeconds(timerToDisplay);
        timerText.text = timeSpan.ToString(@"mm\:ss\:ff");
    }
    
    
}
