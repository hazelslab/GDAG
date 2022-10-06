using System.Collections;
using UnityEngine;

public abstract class WalkBaseState : EnemyBaseState
{
    public override void StartState()
    {
        Debug.Log("Start Walk");
    }

    public override void UpdateState()
    {
        
    }

    public override void EndState()
    {
        
    }
}