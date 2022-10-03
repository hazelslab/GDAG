using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The PlayerMaster handles all Ccomponents needed for the player 
/// like PlayerControlls, PlayerAnimations and more.
/// </summary>
public class PlayerMaster : MonoBehaviour
{
    public static PlayerMaster Instance { get; private set; }

    public PlayerController REF_PlayerController;
    public PlayerAnimations REF_PlayerAnimations;
    
    private void Awake()
    {
        // Player is not a singleton, because there is never more then one instantiation
        Instance = this;

        REF_PlayerController = GetComponent<PlayerController>();
        REF_PlayerAnimations = GetComponent<PlayerAnimations>();
    }
}
