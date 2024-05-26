using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverPlayer : MonoBehaviour
{
    [SerializeField] private PlayerStateController _playerStateController = null;
    public PlayerStateController PlayerStateController => _playerStateController;

    [SerializeField] private PlayerInput _playerInput = null;
    public PlayerInput PlayerInput => _playerInput;

    [SerializeField] private CharacterController _cc;
    public CharacterController CharacterController => _cc;

    [SerializeField] private Health _health;
    public Health Health => _health;

    [SerializeField] private PlayerSwimState _playerSwimState;
    public PlayerSwimState PlayerSwimState => _playerSwimState;

    [SerializeField] private Weapon _weapon;
    public Weapon Weapon => _weapon;

    public bool isInvincible;
    public GameObject _shieldVisualizer;

    public bool _isShieldActive = false;

    void Awake()
    {
        _health = GetComponent<Health>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (_playerInput.MouseButtonDown)
        {
            _playerStateController.ChangeState(EDiverPlayerState.Attack);
        }
        else if (_playerInput.SpaceKeyDown)
        {
            _playerStateController.ChangeState(EDiverPlayerState.Dash);
        }
        _playerInput.ClearCache();
    }

    public void ApplyDamage(int damage, Vector3 attackerPos = new Vector3())
    {
        _playerStateController.ChangeState(EDiverPlayerState.BeingHit);
    }


    private void AddHealth(int health)
    {
        _health.AddHealth(health);
        //GetComponent<PlayerVFXManager>().PlayHealVFX();
    }
   
    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.gameObject.SetActive(true);

    }

}
