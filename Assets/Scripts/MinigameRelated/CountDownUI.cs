using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownUI : MonoBehaviour
{

    public GameObject countdownUI;

    private void OnEnable()
    {
        TimerEventManager.TimerCompleted += DeactivateUI;
        
    }

    private void OnDisable()
    {
        TimerEventManager.TimerCompleted -= DeactivateUI;
    }

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeactivateUI()
    {
        countdownUI.SetActive(false);
    }
}
