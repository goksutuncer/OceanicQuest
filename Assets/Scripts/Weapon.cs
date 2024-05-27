using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private DiverPlayer _player;

    public void InitDiver(DiverPlayer diverPlayer)
    {
        _player = diverPlayer;
    }

    public void Fire()
    {
        StartCoroutine(FireRoutine());
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
                shark.ApplyDamage(_player.Damage());
                Debug.Log(_player.Damage());
            }
            Destroy(gameObject);
        }
    }
}
