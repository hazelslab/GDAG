using System.Collections;
using UnityEngine;

public abstract class EnemyBaseMaster : MonoBehaviour
{

    public EnemyStateMachine StateMachine;
    protected PlayerMaster Player;

    [SerializeField]
    public float ChasePlayerDistance = 5f;


    private void Awake()
    {
        Player = PlayerMaster.Instance;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ChasePlayerDistance);
    }
}