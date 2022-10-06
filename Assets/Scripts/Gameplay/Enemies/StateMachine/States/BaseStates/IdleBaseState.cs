using System.Collections;
using UnityEngine;

public abstract class IdleBaseState : EnemyBaseState
{
    public override void StartState()
    {
        Debug.Log("Start Idle");
    }

    public override void UpdateState()
    {
        
    }

    public override void EndState()
    {
        
    }
}