using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private PlayerMaster _master;

    private Animator _anim;

    private void Awake()
    {
        _master = GetComponent<PlayerMaster>();
        _anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        
    }
}

