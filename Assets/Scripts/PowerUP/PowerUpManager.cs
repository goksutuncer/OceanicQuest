using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    // Singleton instance
    public static PowerUpManager Instance;

    // Prefabs for each type of power-up
    public GameObject healthPowerUpPrefab;
    public GameObject shieldPowerUpPrefab;
    public GameObject speedPowerUpPrefab;
    public GameObject doubleDamagePrefab;

    // The maximum number of power-ups to pool for each type
    public int poolSize = 10;

    // Dictionary to store pooled power-ups for each type
    private Dictionary<GameObject, List<GameObject>> pooledPowerUps = new Dictionary<GameObject, List<GameObject>>();

    private void Awake()
    {
        // Ensure only one instance of PowerUpManager exists
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        // Populate the pools for each power-up type
        PopulatePool(healthPowerUpPrefab);
        PopulatePool(shieldPowerUpPrefab);
        PopulatePool(speedPowerUpPrefab);
        PopulatePool(doubleDamagePrefab);
    }

    // Method to retrieve a power-up from the pool for a specific type
    public GameObject GetPowerUpFromPool(GameObject prefab)
    {
        if (!pooledPowerUps.ContainsKey(prefab))
        {
            Debug.LogWarning("Attempted to retrieve a power-up from an unregistered prefab: " + prefab.name);
            return null;
        }

        foreach (GameObject powerUp in pooledPowerUps[prefab])
        {
            if (!powerUp.activeInHierarchy)
            {
                powerUp.SetActive(true);
                return powerUp;
            }
        }

        // If no inactive power-ups are found, create a new one
        GameObject newPowerUp = Instantiate(prefab);
        pooledPowerUps[prefab].Add(newPowerUp);
        return newPowerUp;
    }

    // Method to return a power-up to the pool
    public void ReturnPowerUpToPool(GameObject powerUp)
    {
        powerUp.SetActive(false);
    }

    // Method to populate the pool with power-ups for a specific type
    private void PopulatePool(GameObject prefab)
    {
        List<GameObject> powerUpList = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject powerUp = Instantiate(prefab);
            powerUp.SetActive(false);
            powerUpList.Add(powerUp);
        }

        pooledPowerUps[prefab] = powerUpList;
    }
}
