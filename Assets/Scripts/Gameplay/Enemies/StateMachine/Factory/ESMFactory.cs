using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ESM/Factory")]
public class ESMFactory : ScriptableObject
{

    public EnemyBaseState InitState;

    public List<EnemyBaseState> States;

    protected EnemyBaseMaster Master;
    protected PlayerMaster Player;

    public void InitFactory(EnemyBaseMaster master, PlayerMaster player)
    {
        Master = master;
        Player = player;
    }

    public void CreateStates()
    {
        foreach (var state in States)
        {
            state.InitState(Master, Player);
        }
    }
}