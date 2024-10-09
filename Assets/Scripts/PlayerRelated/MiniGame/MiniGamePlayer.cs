using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class MiniGamePlayer : MonoBehaviour
{

    public PlayerInput playerInput;
    private InputAction playerActons;
    private InputAction boostAction;

    public LayerMask wallMask;
    
    
    public Vector3 playerGeneralMovement; 
    
    public TurtleRacePlayerSO playerData;

    private Rigidbody rb;

    public float speed;

    public Camera mainCam;

    Vector3 MovementDirection;
    Vector3 targetPoint;


    public TurtleRacePlayerSO.SpeedState currentSpeed;

    private void OnEnable()
    {
        TimerEventManager.TimerCompleted += ActivateInput;
    }

    private void OnDisable()
    {
        TimerEventManager.TimerCompleted -= ActivateInput;
    }


    private void Awake()
    {
        speed = playerData.DetermineSpeed(currentSpeed);
    }


    // Start is called before the first frame update
    void Start()
    {
        mainCam = FindObjectOfType<Camera>();
        playerInput = GetComponent<PlayerInput>();
        playerActons = playerInput.actions["Movement"];
        boostAction = playerInput.actions["Boost"];
        
        playerInput.actions.FindActionMap("MinigameActions").Disable();
        
        boostAction.performed += BoostingBaby;

        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {

        
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
      
    }

    public void Movement()
    {
        playerGeneralMovement = playerActons.ReadValue<Vector2>(); //this is handled in fixed update 
        //
        Vector3 newPotentialMove = new Vector3(playerGeneralMovement.y, 0f, -playerGeneralMovement.x);
        
          List<RaycastHit> hits = new List<RaycastHit>(); 
            
        hits = Physics.SphereCastAll(transform.position, 2f, new Vector3(0.001f, 0.001f, 0.001f), 0.1f,
            wallMask).ToList();
        if(hits.Count > 0)
        {
            Debug.LogWarning($"we are colliding with {hits[0].collider.gameObject.name}");
            switch (hits[0].collider.tag)
            {
                case "NorthWall":
                    if(playerGeneralMovement.y > 0) newPotentialMove = new Vector3(hits[0].point.x, 0f, -playerGeneralMovement.x);    
                    break;
                
                case "SouthWall":
                    if(playerGeneralMovement.y < 0) newPotentialMove = new Vector3(hits[0].point.x, 0f, -playerGeneralMovement.x);
                    break;
                
                case "WestWall":
                    if(-playerGeneralMovement.x > 0) newPotentialMove = new Vector3(playerGeneralMovement.y, 0f, hits[0].point.z);
                    break;
            }
            
        }
        
        rb.MovePosition(transform.position + newPotentialMove * Time.deltaTime * speed);
        
        
        
    }

    public void BoostingBaby(InputAction.CallbackContext context)
    {
        if (context.interaction is HoldInteraction)
        {
            currentSpeed = TurtleRacePlayerSO.SpeedState.Boosted;

            speed = playerData.DetermineSpeed(currentSpeed);
        }
        else
        {
            currentSpeed = TurtleRacePlayerSO.SpeedState.Normal;

            speed = playerData.DetermineSpeed(currentSpeed);
        }
    }
    
    /// <summary>
    /// when the player interacts with an obstacle
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Obstacles>(out Obstacles ob))
        {
            currentSpeed = TurtleRacePlayerSO.SpeedState.Slowed;

            speed = playerData.DetermineSpeed(currentSpeed);
            StartCoroutine(ResetToNormalState(playerData._speedCooldown));
        }

        if (other.TryGetComponent(out Finishline finish))
        {
            // finish.AddToFinishedRacers(gameObject);
            finish.processEnd?.Invoke();
        }
        
    }

    /// <summary>
    /// cooldown to reset the speed to its normal state when against slowed effects 
    /// </summary>
    /// <param name="cooldown"></param>
    /// <returns></returns>
    private IEnumerator ResetToNormalState(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);

        currentSpeed = TurtleRacePlayerSO.SpeedState.Normal;
        speed = playerData.DetermineSpeed(currentSpeed);
        
        StopCoroutine(ResetToNormalState(cooldown)); //a firm stop on coroutine in case it plays again
    }

    public void ActivateInput()
    {
        playerInput.actions.FindActionMap("MinigameActions").Enable();
    }
    
}
