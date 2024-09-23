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
    private void Awake()
    {
        mainCam = FindObjectOfType<Camera>();
        tortalBrain = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // tortalBrain.destination = ;
        Debug.LogWarning(mainCam.ScreenToWorldPoint(new Vector3(0, Random.Range(0, Screen.height), 20))+ new Vector3(30, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ClickDraggable"))
        {
            Destroy(other.gameObject);
        }
    }

    public void SetNewDestination(Vector3 pos)
    {
        tortalBrain.SetDestination(pos);
    }
}
