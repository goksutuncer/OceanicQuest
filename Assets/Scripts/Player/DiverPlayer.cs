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

    private Health _health;
    public bool isInvincible;

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
            return;
        }
        else if (_playerInput.MouseButtonUp)
        {
            _playerStateController.ChangeState(EDiverPlayerState.Swim);
        }
        else if (_playerInput.SpaceKeyDown)
        {
            _playerStateController.ChangeState(EDiverPlayerState.Dash);
            return;
        }
    }

    public void ApplyDamage(int damage, Vector3 attackerPos = new Vector3())
    {
        /*if (isInvincible)
        {
            return;
        }*/
        if (_health != null)
        {
            _health.ApplyDamage(damage);
            Debug.Log("Health");
        }
        
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
}
