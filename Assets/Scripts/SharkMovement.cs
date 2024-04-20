using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkMovement : MonoBehaviour
{
    private CharacterController _cc;
    public float moveSpeed = 5f; // Speed of movement
    public float rotationSpeed = 5f; // Speed of rotation
    public float changeDirectionInterval = 3f; // Interval to change direction

    // Random direction for movement
    private Vector3 randomDirection;
    private float timer; // Timer for changing direction
    private float screenWidth;
    private float screenHeight;
    private bool wasWithinBoundary = true;

    private Health _health;
    //Item drop
    public GameObject ItemToDrop;
    public int Coin;


    // Material animation
    private MaterialPropertyBlock _materialPropertyBlock;
    private SkinnedMeshRenderer _skinnedMeshRenderer;

    // State Machine
    public enum CharacterState // type
    {
        Normal, Attacking, Dead, BeingHit
    }
    public CharacterState currentState; // variable for the type

    void Awake()
    {
        _cc = GetComponent<CharacterController>();
        // Initialize random direction at start
        GenerateRandomDirection();
        screenWidth = CalculateScreenWidth();
        screenHeight = CalculateScreenHeight();
        _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
        _skinnedMeshRenderer.GetPropertyBlock(_materialPropertyBlock);
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case CharacterState.Normal:

                CalculateMovementShark();
                break;

            case CharacterState.Attacking:
                break;

            case CharacterState.Dead:
                break;
            case CharacterState.BeingHit:
                break;

        }

    }
    void CalculateMovementShark()
    {
        // Move the object
        transform.Translate(-1 * randomDirection * moveSpeed * Time.deltaTime, Space.World);

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -screenWidth / 2f, screenWidth / 2f);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -screenHeight / 2f, screenHeight / 2f);
        transform.position = clampedPosition;

        // Rotate the object towards the movement direction
        Quaternion targetRotation = Quaternion.LookRotation(randomDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Update timer
        timer += Time.deltaTime;

        // Check if it's time to change direction
        if (timer >= changeDirectionInterval)
        {
            GenerateRandomDirection();
            timer = 0f; // Reset timer
        }
        bool isWithinBoundary = (transform.position.x >= -12f && transform.position.x <= 12f);
        if (!isWithinBoundary && wasWithinBoundary)
        {
            // If the object was within the boundary in the previous frame but has now crossed it, reset its position
            float rand = Random.Range(0f, 1f);
            float randomY = Random.Range(-4f, 5f);
            float randomX = rand < 0.5f ? -17f : 14f;
            transform.position = new Vector3(randomX, randomY, transform.position.z);
        }

        // Update the flag for the next frame
        wasWithinBoundary = isWithinBoundary;
    }

    // Generate a random direction
    void GenerateRandomDirection()
    {
        randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        randomDirection.z = 0;
        randomDirection.y = 0;
    }

    // Calculate screen width based on camera FOV and distance
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

    // Calculate screen height based on camera FOV and distance
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
    public void SwitchStateTo(CharacterState newState)
    {
        //exiting a state
        switch (currentState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attacking:
                break;
            case CharacterState.Dead:
                break;
            case CharacterState.BeingHit:
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
            case CharacterState.BeingHit:
                break;
        }
        currentState = newState;

    }
    public void ApplyDamage(int damage, Vector3 attackerPos = new Vector3())
    {
        if (_health != null)
        {
            _health.ApplyDamage(damage);
            Debug.Log("Health??");
        }

        StartCoroutine(MaterialBlink());
        SwitchStateTo(CharacterState.BeingHit);
        AddImpact(attackerPos, 10f);

    }
    private void AddImpact(Vector3 attackerPos, float force)
    {
        Vector3 impactDirection = transform.position - attackerPos;
        impactDirection.Normalize();
        impactDirection.y = 0;
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
}