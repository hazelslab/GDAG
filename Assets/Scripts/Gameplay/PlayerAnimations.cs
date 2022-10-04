using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private PlayerMaster _master;

    private string _currentState;

    private Animator _anim;

    private void Awake()
    {
        _master = GetComponent<PlayerMaster>();
        _anim = GetComponentInChildren<Animator>();
    }

    public void ChangeAnimationState(string newState)
    {
        if (_currentState == newState) return;

        _anim.Play(newState);

        _currentState = newState;
    }

}

public enum PlayerAnimState
{
    PLAYER_IDLE,
    PLAYER_WALK,
    PLAYER_RUN,
    PLAYER_CROUCH,
    PLAYER_CRAWL,
    PLAYER_JUMP_RISE,
    PLAYER_JUMP_MID,
    PLAYER_JUMP_FALL
}