using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

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
}
