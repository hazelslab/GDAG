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

    public PlayerStats PlayerStats_REF;
    public PlayerController PlayerController_REF;
    public PlayerAnimations PlayerAnimations_REF;
    
    private void Awake()
    {
        // Player is not a singleton, because there is never more then one instantiation
        Instance = this;

        PlayerStats_REF = GetComponent<PlayerStats>();
        PlayerController_REF = GetComponent<PlayerController>();
        PlayerAnimations_REF = GetComponent<PlayerAnimations>();
    }
}
