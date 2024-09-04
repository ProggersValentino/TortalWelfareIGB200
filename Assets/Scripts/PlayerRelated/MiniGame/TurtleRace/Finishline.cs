using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Finishline : MonoBehaviour
{
    public List<GameObject> FinishedRacers = new List<GameObject>();

    public UnityEvent processEnd;

    // Start is called before the first frame update
    void Start()
    {
        Debug.LogWarning(FindPlayerPlacement());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //to determine the position the player achieved when finishing the race
    public void AddToFinishedRacers(GameObject finishedRacer)
    {
        FinishedRacers.Add(finishedRacer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out RacerAI AI) || other.TryGetComponent(out MiniGamePlayer player))
            AddToFinishedRacers(other.gameObject);
    }

    public int FindPlayerPlacement()
    {
        for (int i = 0; i < FinishedRacers.Count; i++)
        {
            if (FinishedRacers[i].TryGetComponent(out MiniGamePlayer player))
            {
                return i + 1; //to set it to starting from 1 
            }
        }

        return 0; //we couldnt find player
    }
}
