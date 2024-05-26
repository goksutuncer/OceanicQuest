using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGem : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float respawnTimeSeconds = 8;
    [SerializeField] private int experienceGained = 25;

    private void CollectGem()
    {
        //circleCollider.enabled = false;
        //isual.gameObject.SetActive(false);
        GameEventsManager.instance.playerEvents.ExperienceGained(experienceGained);
        GameEventsManager.instance.miscEvents.GemCollected();
        StopAllCoroutines();
        StartCoroutine(RespawnAfterTime());
    }

    private IEnumerator RespawnAfterTime()
    {
        yield return new WaitForSeconds(respawnTimeSeconds);
        //circleCollider.enabled = true;
        //visual.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            CollectGem();
        }
    }
}
