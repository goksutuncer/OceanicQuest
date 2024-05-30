using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer[] _skinnedMeshRenderer = null;
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

    private SharkHealth _sharkHealth;

    //Item drop
    public GameObject ItemToDrop;
    public int Coin;


    [SerializeField] private GameObject Blood_splatter;

    void Awake()
    {
        _sharkHealth = GetComponent<SharkHealth>();
        _cc = GetComponent<CharacterController>();
        // Initialize random direction at start
        GenerateRandomDirection();
        screenWidth = CalculateScreenWidth();
        screenHeight = CalculateScreenHeight();
    }

    void FixedUpdate()
    {
        CalculateMovementShark();
        CheckHealthShark();
    }

    private void CheckHealthShark()
    {
        if (_sharkHealth.CurrentHealth <= 0)
        {
            StartCoroutine(MaterialDissolve());
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            boxCollider.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            DiverPlayer player = other.transform.GetComponent<DiverPlayer>();

            if (player != null)
            {
                player.ApplyDamage(30);
            }
        }
    }

    public void ApplyDamage(int damage, Vector3 attackerPos = new Vector3())
    {

        if (_sharkHealth != null)
        {
            _sharkHealth.ApplyDamage(damage);
            StartCoroutine(BloodSplash());
        }

    }
    IEnumerator BloodSplash()
    {
        Blood_splatter.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Blood_splatter.SetActive(false);
    }
    void CalculateMovementShark()
    {
        transform.Translate(-1 * randomDirection * moveSpeed * Time.deltaTime, Space.World);

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -screenWidth / 2f, screenWidth / 2f);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -screenHeight / 2f, screenHeight / 2f);
        transform.position = clampedPosition;

        // Rotate the object towards the movement direction
        Quaternion targetRotation = Quaternion.LookRotation(randomDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        timer += Time.deltaTime;

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

        wasWithinBoundary = isWithinBoundary;
    }

    void GenerateRandomDirection()
    {
        randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        randomDirection.z = 0;
        randomDirection.y = 0;
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

    IEnumerator MaterialDissolve()
    {
        float dissolveTimeDuration = 2f;
        float currentDissolveTime = 0;
        float dissolveAmount_start = 0f;
        float dissolveAmount_target = 1f;
        float dissolveAmount;

        while (currentDissolveTime < dissolveTimeDuration)
        {
            currentDissolveTime += Time.deltaTime;
            foreach (var item in _skinnedMeshRenderer)
            {
                dissolveAmount = Mathf.Lerp(dissolveAmount_start, dissolveAmount_target, currentDissolveTime / dissolveTimeDuration);
                MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
                materialPropertyBlock.SetFloat("_DissolveAmount", dissolveAmount);
                item.SetPropertyBlock(materialPropertyBlock);
            }
            yield return null;
        }
        GameEventsManager.instance.miscEvents.SharkKilled();
        Destroy(gameObject);
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