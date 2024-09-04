using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [CanBeNull] public TaskSO taskToComplete;

    public PlayerInput input;

    private InputAction movement;

    public UnityEvent OnClickToMove;
    
    private bool hasMoved = false;

    public List<GameObject> goToEnable;
    
    private void Awake()
    {
        movement = input.actions["Movement"];
        movement.performed += GetClickPos;
        SceneManager.sceneLoaded += CurrentSceneLoaded;
    }

    private void OnDisable()
    {
        movement.performed -= GetClickPos;
        SceneManager.sceneLoaded -= CurrentSceneLoaded;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void CurrentSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.LogWarning($"Scene Loaded {scene.name}");
        TaskEventManager.OnStartTask();
    }
    
    // Update is called once per frame
    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     
        // }
    }

    public void GetClickPos(InputAction.CallbackContext context)
    {
        if (TaskEventManager.OnRecieveCurrentTask() == taskToComplete)
        {
            Debug.LogWarning("we have finished movement");
            TaskEventManager.OnCompleteTask();
            hasMoved = true;
        }
    }
    
}
