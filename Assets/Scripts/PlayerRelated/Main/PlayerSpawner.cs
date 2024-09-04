using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    public SpawnObjectSO player;
    public PlayerSO playerData;
    public PlayerInput input;

    public Camera cam;
    
    private void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        //MultipleSceneManager.SetActiveScene("DialogueTurtlePerspective");
        player.SpawnObject(Vector3.zero, gameObject, input, cam);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
