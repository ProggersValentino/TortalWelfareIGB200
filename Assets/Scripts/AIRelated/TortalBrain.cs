using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class TortalBrain : AnimalBrain
{
    public NavMeshPath path;

    public List<Vector3> pathPoints = new List<Vector3>();
    
    private int pathIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        path = new NavMeshPath();
        SetOverallNewDestination(new Vector3(3.45000005f,-0.379999995f,-1.57000005f));
        
       
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AreWeThereYet(pathPoints[pathIndex]) &&  pathIndex < pathPoints.Count -1) pathIndex++;
        MoveToDestination(pathPoints[pathIndex]);
    }

    private void FixedUpdate()
    {
        //MoveToDestination(pathPoints[pathIndex]);
    }


    /// <summary>
    /// sets the overall goal of the turtle
    /// </summary>
    /// <param name="newPos"></param>
    public void SetOverallNewDestination(Vector3 newPos)
    {
        aiBrain.CalculatePath(newPos, path);

        // aiBrain.SetDestination(newPos);
        
        pathPoints = path.corners.ToList();
        //aiBrain.isStopped = true;
    }

    public void MoveToDestination(Vector3 pathCheckPoint)
    {
        Vector3 currentVel = new Vector3();
        transform.position = Vector3.SmoothDamp(transform.position, pathCheckPoint, ref currentVel, 1 / aiBrain.speed);
    }

    public void DeterminePathFlow()
    {
        
    }

    public bool AreWeThereYet(Vector3 currentCheckPoint)
    {
        return Vector3.Distance(transform.position, currentCheckPoint) < 1f;
    }
}
