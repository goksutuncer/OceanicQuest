using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Shark shark;

    public void Fire()
    {
        StartCoroutine(FireRoutine());
    }

    private IEnumerator FireRoutine()
    {
        transform.parent = null;

        float timePassed = 0;

        while (timePassed < 5)
        {
            timePassed += Time.deltaTime;

            transform.Translate(Vector3.forward * 0.1f);

            yield return null;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("SHark get HIT");
            Shark shark = other.transform.GetComponent<Shark>();

            if (shark != null)
            {
                shark.ApplyDamage(30);
            }
            Destroy(gameObject);
        }
    }
}
