using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    public GameObject turtlePref;
    
    public Transform point1;
    public Transform point2;

    public bool isGameGoing { get; private set; }

    
    
    private void OnEnable()
    {
        TurtleFeederEventSystem.GameIsProgressing += GameProgressing;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameProgressing(bool isGoing)
    {
        isGameGoing = isGoing;
    }
}
