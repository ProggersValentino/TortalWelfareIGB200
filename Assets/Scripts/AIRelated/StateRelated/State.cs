using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class State 
{
    
    
    //enum of states that the AI can transition to 
    public enum STATE
    {
        Idle, Walk, Run, LayEggs
    }

    private protected STATE currentState;
    
    public STATE _currentState
    {
        get { return currentState; }
    }
    
    
    //enums for current state of process we're in
    public enum ProcessStates
    {
        Enter, Update, Exit
    }

    private protected ProcessStates currentProcess;

    public ProcessStates _currentProcess
    {
        get { return currentProcess; }
        set { currentProcess = value; }
    }
    
    
    private protected State nextState;
    
    public State _nextState
    {
        get { return nextState; }
        set { nextState = value; }
    }
    
    //variables we want reference to for the sake of data to make transitioning to another easier
    private protected AnimalBrain animalBrain;
    private protected GameObject AIChar;
    
    
    //name of state we are in 
    
    
    //transition methods
    public virtual void Enter() { currentProcess = ProcessStates.Update; } //when we enter the state we perform what we need then transistion to 

    public virtual void Update() { currentProcess = ProcessStates.Update; }

    public virtual void Exit() { currentProcess = ProcessStates.Exit; }
   
    //process method to shift the different PROCESS states 
    public State PROCESS()
    {
        switch (currentProcess)
        {
            case ProcessStates.Enter:
                Enter();
                break;
            case ProcessStates.Update:
                Update();
                break;
            case ProcessStates.Exit:
                Exit(); 
                //perform exit code 
                //exit this state 
                // go to next state 
                return nextState;
                break;
        }

        return this;
    }

    public State(GameObject AI, AnimalBrain animalBrain)
    {
        AIChar = AI;
        currentProcess = ProcessStates.Enter;
        this.animalBrain = animalBrain;
    }


}
