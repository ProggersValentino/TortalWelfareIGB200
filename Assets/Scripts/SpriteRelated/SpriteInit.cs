using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteInit : MonoBehaviour
{
    private Camera mainCam;

    private void OnEnable()
    {
        mainCam = FindObjectOfType<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(mainCam.transform.position);
    }
}
