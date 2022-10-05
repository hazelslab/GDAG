using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private PlayerMaster _master;

    public float CurrentStamina { get { return _currentStamina; } private set { CurrentStamina = value; } }

    [SerializeField]
    private float _currentStamina = 100f;
    [SerializeField]
    private float _maxStamina = 100f;
    [SerializeField]
    private float _drainMultiplier = 3f;
    [SerializeField]
    private float _walkRegenMultiplier = 2f;
    [SerializeField]
    private float _idleRegenMultiplier = 2.5f;
    [SerializeField]
    private float _timeUntilStaminaRegen = 3f;
    [SerializeField]
    private float _timeUntilStaminaRegen_timer = 0f;

    private bool _canRegenStamina;

    //Refs
    [SerializeField]
    private Slider _staminaSlider;



    private void Awake()
    {
        _master = GetComponent<PlayerMaster>();
    }

    private void Start()
    {
        _staminaSlider.maxValue = _maxStamina;
    }

    private void Update()
    {
        _staminaSlider.value = _currentStamina;

        if (_master.PlayerController_REF.IsRunning && _currentStamina > 0f)
        {
            _currentStamina -= (10f * _drainMultiplier) * Time.deltaTime;
            if (_currentStamina <= 0f) _currentStamina = 0f;
            _timeUntilStaminaRegen_timer = 0f;
            _canRegenStamina = false;
        }
        else if (_canRegenStamina && (_master.PlayerController_REF.CurrentPlayerAnimState == PlayerAnimState.PLAYER_IDLE
                                      || _master.PlayerController_REF.CurrentPlayerAnimState == PlayerAnimState.PLAYER_CROUCH) && _currentStamina < _maxStamina)
        {
            _currentStamina += (10f * _idleRegenMultiplier) * Time.deltaTime;
            if (_currentStamina >= _maxStamina) _currentStamina = _maxStamina;
        }
        else if (_canRegenStamina && (_master.PlayerController_REF.CurrentPlayerAnimState == PlayerAnimState.PLAYER_WALK
                                      || _master.PlayerController_REF.CurrentPlayerAnimState == PlayerAnimState.PLAYER_CRAWL) && _currentStamina < _maxStamina)
        {
            _currentStamina += (10f * _walkRegenMultiplier) * Time.deltaTime;
            if (_currentStamina >= _maxStamina) _currentStamina = _maxStamina;
        }

        if (!_canRegenStamina)
        {
            _timeUntilStaminaRegen_timer += Time.deltaTime;
            if (_timeUntilStaminaRegen_timer >= _timeUntilStaminaRegen)
            {
                _canRegenStamina = true;
                _timeUntilStaminaRegen_timer = 0f;
            }
        }
    }

}
