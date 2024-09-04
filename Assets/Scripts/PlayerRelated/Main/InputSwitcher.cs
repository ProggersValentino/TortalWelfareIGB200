using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSwitcher : MonoBehaviour
{
    private static PlayerInput input;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /// <summary>
    /// when we want to switch to another active input system then we can
    /// </summary>
    /// <param name="switchFromAction"></param>
    /// <param name="switchToAction"></param>
    public static void SwitchInput(string switchFromAction, string switchToAction)
    {
        input.actions.FindActionMap(switchFromAction).Disable();
        input.actions.FindActionMap(switchToAction).Enable();
    }
}
