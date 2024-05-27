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

    //weapon damage
    private bool _isDamageBoostActive = false;
    private int _damage = 50;
    private int _multidamage = 100;


    public bool _isShieldActive = false;

    void Awake()
    {
        _health = GetComponent<Health>();

    }
    
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
    public int Damage()
    {
        if (_isDamageBoostActive == true)
        {
            Debug.Log("damage is multi");
            return _multidamage;
        }
        else
        {
            Debug.Log("damage is normal");
            return _damage;
        }
    }

    public void ApplyDamage(int damage, Vector3 attackerPos = new Vector3())
    {
        _playerStateController.ChangeState(EDiverPlayerState.BeingHit);
        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldVisualizer.gameObject.SetActive(false);
        }

    }
    public void DamageBoostActive()
    {
        _isDamageBoostActive = true;
        StartCoroutine(DamageBoostPowerDownRoutine());
        Debug.Log("damageboostactive");
    }
    IEnumerator DamageBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        _isDamageBoostActive = false;
        Debug.Log("damageboost deactive");
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
