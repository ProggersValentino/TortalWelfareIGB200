using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayEggs : State
{

    public LayEggs(GameObject AI, TortalBrain animalBrain) : base(AI, animalBrain)
    {
        currentState = STATE.LayEggs;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
