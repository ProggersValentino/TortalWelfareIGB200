using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MiniGamePlayer : MonoBehaviour
{

    public PlayerInput playerInput;
    private InputAction playerActons;

    public TurtleRacePlayerSO playerData;

    public float speed;

    public Camera mainCam;

    Vector3 MovementDirection;
    Vector3 targetPoint;


    public TurtleRacePlayerSO.SpeedState currentSpeed;


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

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        Vector2 playerGeneralMovement = playerActons.ReadValue<Vector2>();
        transform.position += new Vector3(playerGeneralMovement.y, 0f, -playerGeneralMovement.x) * Time.deltaTime *
                              speed;
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
    
    
}
