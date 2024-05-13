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

    public bool isInvincible;
    public GameObject _shieldVisualizer;

    public bool _isShieldActive = false;

    //Pick up
    public GameObject ItemToDrop;
    public int Coin;

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

    public void PickUpItem(PickUp item)
    {
        switch (item.Type)
        {
            case PickUp.PickUpType.Heal:
                AddHealth(item.Value);
                break;
            case PickUp.PickUpType.Coin:
                AddCoin(item.Value);
                break;
        }
    }

    private void AddHealth(int health)
    {
        _health.AddHealth(health);
        //GetComponent<PlayerVFXManager>().PlayHealVFX();
    }

    private void AddCoin(int coin)
    {
        Coin += coin;
    }

   
    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.gameObject.SetActive(true);

    }

}
