using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Cinemachine;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class RangerPlayerController : MonoBehaviour
{
    public PlayerInput playerInput;
    private InputAction movement;
    private InputAction paneCam;
    public Camera mainCam;

    public RangerNPC selectedNPC;

    //initial set up for when the player is enabled/ spawns in
    private void OnEnable()
    {
        //mainCam = FindObjectOfType<Camera>();
        
        movement = playerInput.actions["Movement"];
        paneCam = playerInput.actions["PaneCamera"];
        movement.performed += GetClickPos;
        paneCam.performed += OnPan;
    }

    private void OnDestroy()
    {
        movement.performed -= GetClickPos;
    }

    private void OnDisable()
    {
        mainCam = null;
        movement.performed -= GetClickPos;
        paneCam.performed -= OnPan;
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
    /// when the player clicks this happens 
    /// </summary>
    /// <param name="context"></param>
    void GetClickPos(InputAction.CallbackContext context)
    {
        
        if(mainCam == null) return; //to prevent from spazzing out when transitioning to another scene
        
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //determine what we hit
            switch (hit.collider.GetComponent<MonoBehaviour>())
            {
                case RangerNPC ranger:
                    Debug.LogWarning($"this ranger is {ranger.gameObject.name}");
                    selectedNPC = ranger.SetRangerAsSelected();
                    break;
                case MicroTask taskToBeDone:
                   DetermineWhatTask(taskToBeDone);
                    break;
                default:
                    selectedNPC = null;
                    Debug.LogWarning($"sadge nuithings");
                    Debug.LogWarning($"context {context.action}");
                    break;
            }
        }
        
    }

    void DetermineWhatTask([CanBeNull] MicroTask task)
    {
        switch (task)
        {
            case HelpTurtleMicroTask helpTurtle:
                //if(selectedNPC != null) selectedNPC.CompleteTask(helpTurtle);
                helpTurtle.StartMicroGame();
                break;
            
            default:
                if(selectedNPC != null) selectedNPC.CompleteTask(task);
                break;
            
        }
    }
    
    void OnPan(InputAction.CallbackContext context)
    {
        if(context.interaction is HoldInteraction)
            Debug.LogWarning("yes");
    }
}
