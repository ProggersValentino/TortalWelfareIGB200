using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class TurtlePlayer : MonoBehaviour
{

    public PlayerInput playerInput;
    public InputAction movement;

    public PlayerSO playerData;

    public float speed;

    public Camera mainCam;

    Vector3 MovementDirection;
    Vector3 targetPoint;


    //have something for player data

    //initial set up for when the player is enabled/ spawns in
     private void OnEnable()
     {
         mainCam = FindObjectOfType<Camera>();
         //playerInput = gameObject.transform.parent.GetComponent<PlayerInput>();
    
         
         
     }
     private void OnDisable()
     {
         mainCam = null;
         movement.performed -= GetClickPos;
     }

     private void OnDestroy()
     {
         mainCam = null;
         
         movement.performed -= GetClickPos;
     }

     private void Awake()
    {
        playerData._lastPlayerLocoBeforeSceneChange = Vector3.zero;

        
        
        
      /*  movement = playerInput.actions["Movement"];

        movement.performed += GetClickPos;*/
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.LogWarning(gameObject.transform.parent.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(targetPoint, transform.position) > 0.3f)
        {
            Vector3 normalDirec = MovementDirection.normalized;

            transform.position += normalDirec * Time.deltaTime * speed;
        }
    }

    public void GetClickPos(InputAction.CallbackContext context)
    {
        if(mainCam == null) return;
        
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

       

        if (Physics.Raycast(ray, out hit))
        {
            //determien what we hit
            switch (hit.collider.gameObject.layer)
            {
                case 6: //door interactable to go to mini games or other parts of the map 
                    travelToInteractable(hit);
                    break;
                case 7: //item to pick up
                    
                    break;
                default:
                    targetPoint = hit.point;

                    playerData._lastPlayerLocoBeforeSceneChange = targetPoint; //setting the last point where the player was 
                    MovementDirection = TravelToPoint(targetPoint);
                    Debug.Log(hit.point);
                    break;
            }

            Debug.DrawLine(ray.origin, hit.point, Color.green, 10f);

            

            if (hit.collider.CompareTag("DoorInteractable"))
            {
                hit.collider.GetComponent<MouseHover>().wantsToGo = true;
            }
        }

    }

    public void travelToInteractable(RaycastHit hit)
    {
        MouseHover interactable = hit.collider.GetComponent<MouseHover>(); 

        targetPoint = interactable.predeterminedLoco.position;
        playerData._lastPlayerLocoBeforeSceneChange = targetPoint; //setting the last point where the player was 
        MovementDirection = TravelToPoint(targetPoint); //getting the direction for the player to move in 
    }

    Vector3 TravelToPoint(Vector3 target)
    {
        return target - transform.position;
    }

    /// <summary>
    /// whenever we want to reset certain data variables we call this
    /// </summary>
    public void resetData()
    {
        playerData._lastPlayerLocoBeforeSceneChange = Vector3.zero;
    }

}
