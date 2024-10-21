using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionInWorld : MonoBehaviour
{
    public GameObject objectToMove;
    
    public Camera mainCam;
    
    private void Awake()
    {
        objectToMove.transform.position = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 20f));
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
