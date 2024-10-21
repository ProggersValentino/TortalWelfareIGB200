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

    public Animator turtleAnim;
    
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
            turtleAnim.SetBool("isWater", true);
            transform.position += normalDirec * Time.deltaTime * speed;
        }
        else turtleAnim.SetBool("isWater", false);
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

    public void FlipSprite(Vector2 direction)
    {

        Vector3 transcribedDirection = new Vector3(direction.y, 0.0f, direction.x);
        
        Vector3 animalScale = transform.localScale;

        bool isLeft = animalScale.z > 0; //to determine the current direction the animal is facing
        
        //if the direction provided x axis is less than 0 and is not left then we flip the scale on the x-axis to a positive
        if (transcribedDirection.z < 0 && !isLeft)
        {
            transform.localScale = new Vector3(animalScale.x, animalScale.y, Mathf.Abs(animalScale.z));
            //Debug.LogWarning($"we flipped it");
            
        }
        else if (transcribedDirection.z > 0 && isLeft)
        {
            transform.localScale = new Vector3(animalScale.x, animalScale.y, -animalScale.z);
            //Debug.LogWarning($"we flipped it");
        }
    }
    
    public void travelToInteractable(RaycastHit hit)
    {
        MouseHover interactable = hit.collider.GetComponent<MouseHover>(); 

        targetPoint = interactable.predeterminedLoco.position;
        //playerData._lastPlayerLocoBeforeSceneChange = targetPoint; //setting the last point where the player was 
        MovementDirection = TravelToPoint(targetPoint); //getting the direction for the player to move in 
    }

    Vector3 TravelToPoint(Vector3 target)
    {
        AudioEventSystem.OnPlayAudio("TurtleSandMove");
        Vector2 direction = new Vector2(target.z - transform.position.z, target.x - transform.position.x);

        Vector2 directionNormalized = direction.normalized;
        
        FlipSprite(directionNormalized);
        
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
