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
        SpawningQuest();
        GameManager.Instance.OnGameOver += OnGameOver;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance)
            GameManager.Instance.OnGameOver -= OnGameOver;
    }

    private void OnGameOver()
    {
        _stopSpawning = true;
    }

    public void SpawningQuest()
    {
        if (_stopSpawning == false)
        {
            Vector3 posofAnimal = new Vector3(Random.Range(-10f, 10f), -6, 0);
            GameObject _questGiverAnimalsPrefab = _questGiverAnimals[Random.Range(0, _questGiverAnimals.Length)];
            GameObject newquestGiverAnimals = Instantiate(_questGiverAnimalsPrefab, posofAnimal, Quaternion.identity);
        }
    }
}
