using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DiverPlayer : MonoBehaviour
{
    private CharacterController _cc;
    public float MoveSpeed = 5f;
    private Vector3 _movementVelocity;
    private PlayerInput _playerInput;
    private Animator _animator;
    private float screenWidth;
    private float screenHeight;


    private Health _health;
    public bool isInvincible;
    public float SlideSpeed = 9f;

    //Pick up
    public GameObject ItemToDrop;
    public int Coin;

    // Material animation
    private MaterialPropertyBlock _materialPropertyBlock;
    private SkinnedMeshRenderer _skinnedMeshRenderer;

    // State Machine
    public enum CharacterState // type
    {
        Normal, Attacking, Dead, Slide
    }
    public CharacterState currentState; // variable for the type

    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;

    void Awake()
    {
        _cc = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        screenWidth = CalculateScreenWidth();
        screenHeight = CalculateScreenHeight();
        
        _playerInput = GetComponent<PlayerInput>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
        _skinnedMeshRenderer.GetPropertyBlock(_materialPropertyBlock);
    }
    float CalculateScreenWidth()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            return 0f;
        }

        float distanceFromCamera = Mathf.Abs(transform.position.z - mainCamera.transform.position.z);
        return Mathf.Tan(mainCamera.fieldOfView * Mathf.Deg2Rad / 2) * distanceFromCamera * 2 * mainCamera.aspect;

    }
    float CalculateScreenHeight()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            return 0f;
        }

        float distanceFromCamera = Mathf.Abs(transform.position.z - mainCamera.transform.position.z);
        return Mathf.Tan(mainCamera.fieldOfView * Mathf.Deg2Rad / 2) * distanceFromCamera * 2;
    }

    private void CalculatePlayerMovement()
    {
        if (_playerInput.MouseButtonDown)
        {
            SwitchStateTo(CharacterState.Attacking);
            return;
        }
        else if (_playerInput.SpaceKeyDown)
        {
            SwitchStateTo(CharacterState.Slide);
            return;
        }
        _movementVelocity.Set(_playerInput.HorizontalInput, _playerInput.VerticalInput, 0f);
        _movementVelocity.Normalize();
        _animator.SetFloat("Speed", _movementVelocity.magnitude);
        _movementVelocity *= MoveSpeed * Time.deltaTime;
        if (_movementVelocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(-1 * _movementVelocity);
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        switch (currentState)
        {
            case CharacterState.Normal:

                CalculatePlayerMovement();
                
                break;
            case CharacterState.Slide:
                _movementVelocity = transform.forward * SlideSpeed * Time.deltaTime;
                break;
        } 
        _cc.Move(_movementVelocity);

        if (Mathf.Abs(transform.position.x) > screenWidth / 2)
        {
            // Wrap player around to the opposite side
            float wrapOffset = Mathf.Sign(transform.position.x) * screenWidth;
            transform.position -= new Vector3(wrapOffset, 0, 0);
        }
        Vector3 clampedPosition = transform.position;
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -screenHeight / 2, screenHeight / 2);
        transform.position = clampedPosition;
    }
    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + new UnityEngine.Vector3(0, 1.05f, 0), UnityEngine.Quaternion.identity);
        /*if (_isTripleShotActive == true)
        {
            Instantiate(_tripleshotPrefab, transform.position, UnityEngine.Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new UnityEngine.Vector3(0, 1.05f, 0), UnityEngine.Quaternion.identity);
        }
        _audioSource.Play(); */
    }

    public void SwitchStateTo(CharacterState newState)
    {
        // clear cache
        _playerInput.ClearCache();

        //exiting a state
        switch (currentState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attacking:
                break;
            case CharacterState.Dead:
                break;
            case CharacterState.Slide:
                break;
        }
        //entering a state 
        switch (newState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attacking:

                //_animator.SetTrigger("Attack");
                //attackStartTime = Time.time; //current game time

                break;
            case CharacterState.Dead:
                _cc.enabled = false;
                StartCoroutine(MaterialDissolve());
                break;
            case CharacterState.Slide:

                break;
        }
        currentState = newState;

    }
    public void ApplyDamage(int damage, Vector3 attackerPos = new Vector3())
    {
        if (isInvincible)
        {
            return;
        }
        if (_health != null)
        {
            _health.ApplyDamage(damage);
        }
        
        //StartCoroutine(MaterialBlink());
        //SwitchStateTo(CharacterState.BeingHit);
        //AddImpact(attackerPos, 10f);

    }
    IEnumerator MaterialBlink()
    {
        _materialPropertyBlock.SetFloat("_blink", 0.4f);
        _skinnedMeshRenderer.SetPropertyBlock(_materialPropertyBlock);

        yield return new WaitForSeconds(0.2f);

        _materialPropertyBlock.SetFloat("_blink", 0f);
        _skinnedMeshRenderer.SetPropertyBlock(_materialPropertyBlock);
    }

    IEnumerator MaterialDissolve()
    {
        yield return new WaitForSeconds(2);
        float dissolveTimeDuration = 2f;
        float currentDissolveTime = 0;
        float dissolveHeight_start = 20f;
        float dissolveHeight_target = -10f;
        float dissolveHeight;

        _materialPropertyBlock.SetFloat("_enableDissolve", 1f);
        _skinnedMeshRenderer.SetPropertyBlock(_materialPropertyBlock);

        while (currentDissolveTime < dissolveTimeDuration)
        {
            currentDissolveTime += Time.deltaTime;
            dissolveHeight = Mathf.Lerp(dissolveHeight_start, dissolveHeight_target, currentDissolveTime / dissolveTimeDuration);
            _materialPropertyBlock.SetFloat("_dissolve_height", dissolveHeight);
            _skinnedMeshRenderer.SetPropertyBlock(_materialPropertyBlock);
            yield return null;
        }
        DropItem();
    }
    public void DropItem()
    {
        if (ItemToDrop != null)
        {
            Instantiate(ItemToDrop, transform.position, Quaternion.identity);
        }
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
    public void RotateToTarget()
    {
        if (currentState != CharacterState.Dead)
        {
            //transform.LookAt(TargetPlayer, Vector3.up);
        }
    }
}
