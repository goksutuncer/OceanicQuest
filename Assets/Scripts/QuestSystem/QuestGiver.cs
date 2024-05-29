using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private DiverPlayer _player;
    private bool _stopSpawning = false;
    [SerializeField] private GameObject[] _questGiverAnimals;

    // Start is called before the first frame update
    void Start()
    {

        GameManager.Instance.OnGameOver += OnGameOver;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.OnGameOver -= OnGameOver;
        }
    }

    private void OnGameOver()
    {
        _stopSpawning = true;
    }

    public void SpawningQuest()
    {
        if (_stopSpawning == false)
        {
            float rand = Random.Range(0f, 1f);
            float randomY = Random.Range(-4f, 5f);
            float randomX = rand < 0.5f ? -17f : 14f;
            Vector3 posToMantaSpawn = new Vector3(randomX, randomY, transform.position.z);
            GameObject _questGiverAnimalsPrefab = _questGiverAnimals[Random.Range(0, _questGiverAnimals.Length)];
            GameObject newquestGiverAnimals = Instantiate(_questGiverAnimalsPrefab, posToMantaSpawn, Quaternion.identity);

        }
    }
}
