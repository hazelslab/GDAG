using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ESM/States/Prototype1/Idle")]
public class Prototype1_IdleState : IdleBaseState
{
    public override void StartState()
    {
        base.StartState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        DefaultChaseBehaviour();
    }

    public override void EndState()
    {
        base.EndState();
    }

    protected virtual void DefaultChaseBehaviour()
    {
        if (Vector2.Distance(Master.transform.position, Player.transform.position) < Master.ChasePlayerDistance)
        {
            Debug.Log("chase");
            Master.StateMachine.SwitchState(typeof(Prototype1_WalkState));
        }
    }
}