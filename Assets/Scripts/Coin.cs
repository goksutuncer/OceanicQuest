using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Coin : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float respawnTimeSeconds = 1;
    [SerializeField] private int goldGained = 1;
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private GameObject visual;
    [SerializeField] private GameObject bubble;
    [SerializeField]
    private float _speed = 2.0f;

    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if (transform.position.y >= 6.50f)
        {
            Destroy(gameObject);
        }
    }

    private void CollectCoin()
    {
        Destroy(gameObject);
        GameEventsManager.instance.goldEvents.GoldGained(goldGained);
        GameEventsManager.instance.miscEvents.CoinCollected();
        StopAllCoroutines();
        StartCoroutine(RespawnAfterTime());
    }
    private IEnumerator RespawnAfterTime()
    {
        yield return new WaitForSeconds(respawnTimeSeconds);
       //Instantiate();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Coin");
            CollectCoin();
        }
    }
}
