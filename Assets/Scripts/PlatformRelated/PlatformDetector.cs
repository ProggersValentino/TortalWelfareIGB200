using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformDetector : MonoBehaviour
{
    private RuntimePlatform plaform;
    
    //Start is called before the first frame update
    void Start()
    {
        Debug.Log(plaform);
        Debug.Log(Application.platform);
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            Debug.Log("hi");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
