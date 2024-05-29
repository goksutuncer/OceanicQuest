using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPoolManager : MonoBehaviour
{
    // Singleton instance
    public static FishPoolManager Instance;

    // Prefabs for each type of fishes
    public GameObject bluePowderTangPrefab;
    public GameObject koiFishPrefab;


    // The maximum number of fishes to pool for each type
    public int poolSize = 10;

    // Dictionary to store pooled fishes for each type
    private Dictionary<GameObject, List<GameObject>> pooledFishes = new Dictionary<GameObject, List<GameObject>>();

    private void Awake()
    {
        // Ensure only one instance of FishPoolManager exists
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        // Populate the pools for each fish type
        PopulatePool(bluePowderTangPrefab);
        PopulatePool(koiFishPrefab);
    }

    // Method to retrieve a fish from the pool for a specific type
    public GameObject GetFishFromPool(GameObject prefab)
    {
        if (!pooledFishes.ContainsKey(prefab))
        {
            Debug.LogWarning("Attempted to retrieve a fish from an unregistered prefab: " + prefab.name);
            return null;
        }

        foreach (GameObject fish in pooledFishes[prefab])
        {
            if (!fish.activeInHierarchy)
            {
                fish.SetActive(true);
                return fish;
            }
        }

        // If no inactive fish are found, create a new one
        GameObject newFish = Instantiate(prefab);
        pooledFishes[prefab].Add(newFish);
        return newFish;
    }

    // Method to return a fish to the pool
    public void ReturnFishToPool(GameObject fish)
    {
        fish.SetActive(false);
    }

    // Method to populate the pool with fishes for a specific type
    private void PopulatePool(GameObject prefab)
    {
        List<GameObject> fishList = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject fish = Instantiate(prefab);
            fish.SetActive(false);
            fishList.Add(fish);
        }

        pooledFishes[prefab] = fishList;
    }
}
