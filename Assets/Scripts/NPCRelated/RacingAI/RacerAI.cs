using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class RacerAI : MonoBehaviour
{
    private NavMeshAgent brain;

    
    public NavMeshPath path;

    public List<Vector3> pathPoints = new List<Vector3>();
    
    private int pathIndex = 0;

    private Rigidbody rb;
    
    Vector3 currentVel = new Vector3();
    
    //public Transform goal;

    private void Awake()
    {
        brain = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        brain.updatePosition = false;
        path = new NavMeshPath();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if (AreWeThereYet(pathPoints[pathIndex]) &&  pathIndex < pathPoints.Count -1) pathIndex++;
        MoveToDestination();
        // brain.Move(new Vector3(0.01f, 0, 0.01f) * Time.deltaTime);
    }

    public void SetAIDestination(Transform goal)
    {
        brain.SetDestination(goal.position);
    }
    
    /// <summary>
    /// sets the overall goal of the turtle
    /// </summary>
    /// <param name="newPos"></param>
    public void SetOverallNewDestination(Vector3 newPos)
    {
        brain.CalculatePath(newPos, path);

        // aiBrain.SetDestination(newPos);

        pathPoints = path.corners.ToList();
        //aiBrain.isStopped = true;
    }

    public void MoveToDestination()
    {
        
        transform.position = Vector3.SmoothDamp(transform.position, brain.nextPosition, ref currentVel, 1 / brain.speed);
        // rb.MovePosition(transform.position + pathCheckPoint * Time.deltaTime * brain.speed);
        
        
    }

    public void DeterminePathFlow()
    {
        
    }

    public bool AreWeThereYet(Vector3 currentCheckPoint)
    {
        return Vector3.Distance(transform.position, currentCheckPoint) < 1f;
    }
    
}
