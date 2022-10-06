using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ESM/States/Prototype1/Walk")]
public class Prototype1_WalkState : WalkBaseState
{
    public override void StartState()
    {
        base.StartState();
        Debug.Log("chasing");
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void EndState()
    {
        base.EndState();
    }
}