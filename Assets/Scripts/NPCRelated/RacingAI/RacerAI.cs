using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RacerAI : MonoBehaviour
{
    private NavMeshAgent brain;

    //public Transform goal;

    private void Awake()
    {
        brain = GetComponent<NavMeshAgent>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAIDestination(Transform goal)
    {
        brain.SetDestination(goal.position);
    }
}
