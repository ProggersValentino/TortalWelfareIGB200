using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBank/New State Transition")]
public class StateTransistionSO : ScriptableObject
{
    public List<MainState> TransitionSystem;

    /// <summary>
    /// in the initial setup of the enemy we activate its neurons to the behaviors we want it to do
    /// translating the MainState List into a dictionary to give to the enemy
    /// </summary>
    /// <param name="enemy"></param>
    /// <param name="brain"></param>
    /// <returns></returns>
    public Dictionary<State.STATE, Dictionary<string, State>> CreateKnowledgeBase(GameObject enemy, AnimalBrain brain)
    {
        Dictionary<State.STATE, Dictionary<string, State>> newKnowledgeBase =
            new Dictionary<State.STATE, Dictionary<string, State>>();
    
        foreach (MainState state in TransitionSystem)
        {
            newKnowledgeBase[state.mainState] = new Dictionary<string, State>();
    
            for (int i = 0; i < state.situationsToTransition.Count; i++)
            {
                newKnowledgeBase[state.mainState][state.situationsToTransition[i].situationKey] =
                    WhichState(state.situationsToTransition[i].transitionState, enemy, brain);
            }
        }
    
        return newKnowledgeBase;
    }


    public State WhichState(State.STATE inputtedState, GameObject enemy, AnimalBrain brain)
    {
        switch (inputtedState)
        {
            case State.STATE.Walk:
                
            case State.STATE.Run:
                
            case State.STATE.LayEggs:
                
            default:
                Debug.LogWarning("no valid");
                break;
        }
    
        return null;
    }
    
    
    
}

[Serializable]
public class MainState
{
    public State.STATE mainState;

    public List<TransistionStates> situationsToTransition = new List<TransistionStates>();
}

[Serializable]
public class TransistionStates
{
    public string situationKey; //search key for the specific situation
    public State.STATE transitionState; //the state to change to 
}