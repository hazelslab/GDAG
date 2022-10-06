using System.Collections;
using UnityEngine;

public abstract class EnemyBaseState : ScriptableObject
{
    protected EnemyBaseMaster Master;
    protected PlayerMaster Player;

    public virtual void InitState(EnemyBaseMaster master, PlayerMaster player)
    {
        Master = master;
        Player = player;
    }

    public abstract void StartState();
    public abstract void UpdateState();
    public abstract void EndState();

}