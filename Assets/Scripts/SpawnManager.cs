using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _enemyPrefabs;
    [SerializeField]
    private GameObject _enemyContainer;
    private DiverPlayer _diverPlayer;
    private bool _stopSpawning = false;

    private GameManager _gameManager;
    //private GameObject[] _powerups;


    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        if (_gameManager != null)
        {
            _diverPlayer = GameObject.Find("Diver").GetComponent<DiverPlayer>();
        }
        StartCoroutine(SpawnEnemyRoutine());
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        //StartCoroutine(SpawnPowerupRoutine());

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
            yield return new WaitForSeconds(10.0f);

        }
       
    }

    /*IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 posofPowerup = new Vector3(UnityEngine.Random.Range(-8f, 8f), 7, 0);

            if (_diverPlayer._isShieldActive == false)
            {
                Instantiate(_powerups[UnityEngine.Random.Range(0, 3)], posofPowerup, Quaternion.identity);

            }
            else
            {
                Instantiate(_powerups[UnityEngine.Random.Range(0, 2)], posofPowerup, Quaternion.identity);
            }

            yield return new WaitForSeconds(UnityEngine.Random.Range(3, 8));

        }

    }*/

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
