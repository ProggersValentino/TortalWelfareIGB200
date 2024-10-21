using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalBrain : MonoBehaviour
{
    
    private Dictionary<State.STATE, Dictionary<string, State>> stateTransitionSys =
        new Dictionary<State.STATE, Dictionary<string, State>>();

    public StateTransistionSO transitionData;

    
    public State currentState { get; private set; }

    public NavMeshAgent aiBrain;

    private void Awake()
    {
        aiBrain = GetComponent<NavMeshAgent>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentState.PROCESS();
    }
    
    public State GenerateNextState(string key)
    {
        stateTransitionSys[currentState._currentState].TryGetValue(key, out State transitionState);
        //Debug.LogWarning(transitionState._currentState);
        return transitionState;
    }

    public void TransitionToNextState(string situationKey)
    {

        State temp = GenerateNextState(situationKey);

        if (temp == null) return;
        
        currentState._currentProcess = State.ProcessStates.Exit;
        currentState._nextState = temp;
        stateTransitionSys[currentState._currentState][situationKey] =
            transitionData.WhichState(currentState._nextState._currentState, gameObject, this);
        //state._nextState._nextState = null;
    }
    
}
