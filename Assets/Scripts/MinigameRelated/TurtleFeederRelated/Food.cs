using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Food : MonoBehaviour
{

    public bool isGettingPickedUp { get; set; }
    public bool isOverBin { get; set; }
    
    public NavMeshAgent foodBrain { get; private set; }

    private void Awake()
    {
        foodBrain = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetNewDestination(Vector3 pos)
    {
        foodBrain.SetDestination(pos);
        
    }
    
}
