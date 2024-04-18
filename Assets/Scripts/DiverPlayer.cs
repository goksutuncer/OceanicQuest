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
    private float _verticalVelocity;
    private Animator _animator;
    private float screenWidth;
    private float screenHeight;



    // State Machine
    public enum CharacterState // type
    {
        Normal, Attacking, Dead
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

    }
    private void CalculatePlayerMovement()
    {
        if (_playerInput.MouseButtonDown)
        {
            SwitchStateTo(CharacterState.Attacking);
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
            _playerInput.MouseButtonDown = false;

        //exiting a state
        switch (currentState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attacking:
                break;
            case CharacterState.Dead:
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
                break;
        }
        currentState = newState;

    }
}
