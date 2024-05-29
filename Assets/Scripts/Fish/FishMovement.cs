using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class FishMovement : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer[] _skinnedMeshRenderer = null;
    [SerializeField] private bool _isKoi = false;

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

    //Item drop
    public GameObject ItemToDrop;
    public int Coin;

    void Awake()
    {
        _cc = GetComponent<CharacterController>();
        // Initialize random direction at start
        GenerateRandomDirection();
        screenWidth = CalculateScreenWidth();
        screenHeight = CalculateScreenHeight();
    }

    void FixedUpdate()
    {
        CalculateMovementFish();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(MaterialDissolve());
            SphereCollider sphereCollider = GetComponent<SphereCollider>();
            sphereCollider.enabled = false;
        }
    }


    void CalculateMovementFish()
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
        if (_isKoi)
        {
            GameEventsManager.instance.miscEvents.KoiFishCollected();
        }
        gameObject.SetActive(false);
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
