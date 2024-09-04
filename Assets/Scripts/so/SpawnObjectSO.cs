using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "DataBank/Spawn Object Data")]
public class SpawnObjectSO : ScriptableObject
{
    public GameObject objectToSpawn;

    public void SpawnObject(Vector3 spawnPosition, GameObject parent, PlayerInput input, Camera cam)
    {
        GameObject player = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        
        if(parent != null) player.transform.SetParent(parent.transform, true);

        TurtlePlayer playerInput = player.GetComponent<TurtlePlayer>();

        playerInput.mainCam = cam;
        
        playerInput.playerInput = input;
        playerInput.movement = playerInput.playerInput.actions["Movement"];
        playerInput.movement.performed += playerInput.GetClickPos;
        
    }
}
