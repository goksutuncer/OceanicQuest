using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.0f;
    [SerializeField] // 0 = Health, 1 = Speed, 2 = Shield, 3 = Double Dmg
    private int powerUpID;
    [SerializeField]
    private AudioClip _powerupSound;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if (transform.position.y >= 6.50f)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(_powerupSound, transform.position);
            DiverPlayer player = other.transform.GetComponent<DiverPlayer>();
            if (player != null) 
            {
                switch (powerUpID)
                {
                    case 0:
                        player.Health.AddHealth(20);
                        break;
                    case 1:
                        player.PlayerSwimState.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    case 3:
                        player.DamageBoostActive();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;

                }
            }

            gameObject.SetActive(false);
        }

    }
}
