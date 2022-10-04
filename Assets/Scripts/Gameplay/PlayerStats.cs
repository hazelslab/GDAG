using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private PlayerMaster _master;

    public float CurrentStamina { get { return _currentStamina; } private set { CurrentStamina = value; } }

    [SerializeField]
    private float _currentStamina = 100;
    [SerializeField]
    private float _maxStamina = 100;
    [SerializeField]
    private float _drainMultiplier = 3;



    private void Awake()
    {
        _master = GetComponent<PlayerMaster>();
    }

    private void Update()
    {
        
    }

}
