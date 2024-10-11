using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class MiniGamePlayer : MonoBehaviour
{

    public PlayerInput playerInput;
    private InputAction playerActons;
    private InputAction boostAction;

    public LayerMask wallMask;

    public CinemachineVirtualCamera cam;
    private CinemachineBasicMultiChannelPerlin noise;

    public List<GameObject> obstructions = new List<GameObject>();

    private GameObject currentActiveObstruction;
    
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

        noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
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

            
            
            switch (ob.GetComponent<SpriteRenderer>().sprite.name)
            {
                case "garbage_fishnet":
                    obstructions[0].SetActive(true);
                    currentActiveObstruction = obstructions[0];
                    break;
                
                case "garbage_bag2":
                case "garbage_bag":
                    obstructions[1].SetActive(true);
                    currentActiveObstruction = obstructions[1];
                    break;
                
                case "garbage_bottle":
                    obstructions[2].SetActive(true);
                    currentActiveObstruction = obstructions[2];
                    break;
            }
            
            
            speed = playerData.DetermineSpeed(currentSpeed);
            float newFOV = playerData.DetermineFOV(currentSpeed);
            StartCoroutine(Zoom(newFOV));
            StartCoroutine(ResetToNormalState(playerData._speedCooldown));
            StartCoroutine(ShakeyTime());
        }

        if (other.TryGetComponent(out Finishline finish))
        {
            // finish.AddToFinishedRacers(gameObject);
            finish.processEnd?.Invoke();
        }
        
    }


    /// <summary>
    /// determine the zooming of FOV
    /// </summary>
    /// <param name="zoomAmount"></param>
    /// <returns></returns>
    public IEnumerator Zoom(float zoomAmount)
    {
        float currentFOV = cam.m_Lens.FieldOfView;

        Debug.LogWarning("yes we zooming");
        
        while (cam.m_Lens.FieldOfView != (zoomAmount - 1))
        {
            Debug.LogWarning("yes we zooming");
            currentFOV = Mathf.Lerp(currentFOV, zoomAmount, Time.deltaTime * 20);

            cam.m_Lens.FieldOfView = currentFOV;
            
            yield return new WaitForSeconds(Time.deltaTime); 
        }
        
        
        
    }
    
    /// <summary>
    /// to make camera shake 
    /// </summary>
    /// <param name="ampLevel"></param>
    public void ShakeyShakey(int ampLevel)
    {
        noise.m_AmplitudeGain = ampLevel;
    }

    public IEnumerator ShakeyTime()
    {
        ShakeyShakey(3);

        yield return new WaitForSeconds(0.1f);
        
        ShakeyShakey(0);
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
        StartCoroutine(Zoom(playerData.DetermineFOV(currentSpeed)));
        foreach (GameObject obstruction in obstructions)
        {
            obstruction.SetActive(false);
        }
        
        StopCoroutine(ResetToNormalState(cooldown)); //a firm stop on coroutine in case it plays again
    }

    public void ActivateInput()
    {
        playerInput.actions.FindActionMap("MinigameActions").Enable();
    }
    
}
