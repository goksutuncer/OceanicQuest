using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _enemyPrefabs;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;
    [SerializeField] private PowerUpManager _powerupManager;
    [SerializeField] private GameObject[] _powerups;
    [SerializeField] private DiverPlayer _player;

    // Start is called before the first frame update
    void Start()
    {
        StartSpawning();
        GameManager.Instance.OnGameOver += OnGameOver;
    }
    private void OnDestroy()
    {
        if(GameManager.Instance)
            GameManager.Instance.OnGameOver -= OnGameOver;
    }

    private void OnGameOver()
    {
        _stopSpawning = true;
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(SpawnEnemyRoutine());
        
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawning == false)
        {
            float rand = Random.Range(0f, 1f);
            float randomY = Random.Range(-4f, 5f);
            float randomX = rand < 0.5f ? -17f : 14f;
            Vector3 posToSpawn = new Vector3(randomX, randomY, transform.position.z);
            GameObject newEnemy = Instantiate(_enemyPrefabs[Random.Range(0, 2)], posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);

        }
       
    }

    IEnumerator SpawnPowerupRoutine()
    {
        //yield return new WaitForSeconds(3.0f);
       
        while (_stopSpawning == false)
        {
            Vector3 posofPowerup = new Vector3(Random.Range(-10f, 10f), -6, 0);
            GameObject powerUpPrefab = _powerups[Random.Range(0, _powerups.Length)];
            GameObject spawnedPowerUp = _powerupManager.GetPowerUpFromPool(powerUpPrefab);
            spawnedPowerUp.transform.position = posofPowerup;
            spawnedPowerUp.SetActive(true);

            yield return new WaitForSeconds(Random.Range(3, 8));
        }

    }


}
