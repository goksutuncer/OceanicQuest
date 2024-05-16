using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    int damage = 50;
    public bool _isDamageBoostActive = false;
    private int _damgageMultiplier = 2;

    public void Fire()
    {
        StartCoroutine(FireRoutine());
    }

    public void DamageBoostActive()
    {
        _isDamageBoostActive = true;
        damage *= _damgageMultiplier;
        StartCoroutine(DamageBoostPowerDownRoutine());
    }
    IEnumerator DamageBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isDamageBoostActive = false;
        damage /= _damgageMultiplier;
    }

    private IEnumerator FireRoutine()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        transform.parent = null;
        boxCollider.enabled = true;

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
            Shark shark = other.transform.GetComponent<Shark>();

            if (shark != null)
            {
                shark.ApplyDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
