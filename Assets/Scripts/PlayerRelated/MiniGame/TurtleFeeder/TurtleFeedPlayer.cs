using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class TurtleFeedPlayer : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction mouseTrack;
    private InputAction interaction;

    public Image inventorySlot;
    
    private GameObject interactable;
    private bool isBeingGrabbed;
    
    public GameObject refffer;

    public Camera mainCam;

    private Vector2 mousePos;
    
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        mouseTrack = playerInput.actions["Movement"];
        interaction = playerInput.actions["Click"];

        interaction.performed += ClickDrag;
        interaction.canceled += ClickDragRelease;

        // Debug.LogWarning($"the pos for inven {mainCam.ScreenToWorldPoint(inventorySlot.rectTransform.anchoredPosition)}");
    }

    // Start is called before the first frame update
    void Start()
    {
        //refffer.transform.position = mainCam.ScreenToWorldPoint(new Vector3(0, 0, 20)) ;

        refffer.transform.position = mainCam.ScreenToWorldPoint(new Vector3(Screen.width / 2, 30, 20));
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mouseTrack.ReadValue<Vector2>();
        
        MouseTrack();
        
        Debug.DrawLine(mainCam.transform.position, mainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 20)));
        
        //Debug.LogWarning(mousePos);

        if (isBeingGrabbed && interactable != null)
        {
            Vector3 interactablePos = interactable.transform.position;
            
            interactable.transform.position = Vector3.Lerp(interactablePos, mainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 20)), Time.deltaTime * 50);
        }
    }

    public void MouseTrack()
    {
        //Vector2 mousePos = mouseTrack.ReadValue<Vector2>();
        Ray ray = mainCam.ScreenPointToRay(new Vector3(mousePos.x, mousePos.y, 20));

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Food") || hit.collider.CompareTag("Rubbish"))
            {
                Debug.LogWarning($"the object we can interact with is {hit.collider.gameObject.name}");
                interactable = hit.collider.gameObject;
            }
            
            else if(!isBeingGrabbed) interactable = null; 
        }
    }

    public void ClickDrag(InputAction.CallbackContext context)
    {
        if (context.interaction is PressInteraction && interactable != null)
        {
            isBeingGrabbed = true;
        }
    }

    public void ClickDragRelease(InputAction.CallbackContext context)
    {
        isBeingGrabbed = false;
    }
    
    public void ActivateInput()
    {
        playerInput.actions.FindActionMap("MinigameActions").Enable();
    }
}
