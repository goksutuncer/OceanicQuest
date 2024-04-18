using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    public float rotationSpeed = 5f; // Speed of rotation
    public float changeDirectionInterval = 3f; // Interval to change direction

    private Vector3 randomDirection; // Random direction for movement
    private float timer; // Timer for changing direction
    private float screenWidth;
    private float screenHeight;
    private bool wasWithinBoundary = true;
    void Start()
    {
        // Initialize random direction at start
        GenerateRandomDirection();
        screenWidth = CalculateScreenWidth();
        screenHeight = CalculateScreenHeight();
    }

    void Update()
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
}

