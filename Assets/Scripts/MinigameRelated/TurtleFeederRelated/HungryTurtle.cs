using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class HungryTurtle : MonoBehaviour
{
    
    /// <summary>
    /// time -> 
    /// </summary>
    
    public NavMeshAgent tortalBrain { get; private set; }
    private Camera mainCam;

    private bool hasBeenFed; //has the turtle been fed yet
    
    public Vector3 direction = Vector3.zero;
    private Vector3 previousPos = Vector3.zero;
    
    Vector3 currentVel = Vector3.zero;
    private void Awake()
    {
        mainCam = FindObjectOfType<Camera>();
        tortalBrain = GetComponent<NavMeshAgent>();
        tortalBrain.updatePosition = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        Debug.LogWarning(mainCam.ScreenToWorldPoint(new Vector3(0, Random.Range(0, Screen.height), 20))+ new Vector3(30, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {

        // direction = new Vector3(transform.position.x - previousPos.x, 0f, transform.position.z - previousPos.z).normalized;  
        // previousPos = transform.position;
    }

    private void FixedUpdate()
    {
        MoveToPos();
    }

    private void OnTriggerEnter(Collider other)
    {
        //is there a turtle that hasnt been fed
        if (other.CompareTag("Food") && !hasBeenFed)
        {
            Destroy(other.gameObject);
            hasBeenFed = true; //turtle fed
            SetNewDestination(tortalBrain.destination * -2);
            TurtleFeederEventSystem.OnUpdateTurtleFedUI(1);
            
        }
        else if(other.CompareTag("Rubbish") && !hasBeenFed)
        {
            Destroy(other.gameObject);
            hasBeenFed = true; //turtle fed
            SetNewDestination(tortalBrain.destination * -2);
            TurtleFeederEventSystem.OnUpdateAteRubbishUI(1);

        }
        
    }

    public void SetNewDestination(Vector3 pos)
    {
        tortalBrain.SetDestination(pos);
    }

    public void MoveToPos()
    {
        transform.position = Vector3.SmoothDamp(transform.position, tortalBrain.nextPosition, ref currentVel, 1 / tortalBrain.speed);
    }
}
