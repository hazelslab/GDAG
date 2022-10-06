using System;
using System.Collections;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{

    [SerializeField]
    public EnemyBaseMaster Master;
    [SerializeField]
    public PlayerMaster Player;

    public ESMFactory ESMFactory;

    public EnemyBaseState CurrentState;


    private void Start()
    {
        ESMFactory.InitFactory(Master, Player);
        ESMFactory.CreateStates();

        CurrentState = ESMFactory.InitState;

        CurrentState.StartState();
    }

    private void Update()
    {
        CurrentState.UpdateState();
    }

    public void SwitchState(Type newState)
    {
        CurrentState.EndState();
        foreach (var state in Master.StateMachine.ESMFactory.States)
        {
            if (state.GetType() == newState)
                CurrentState = state;
        }
        CurrentState.StartState();
    }
}