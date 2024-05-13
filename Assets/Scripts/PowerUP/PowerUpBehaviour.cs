using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField] // 0 = TripleShot, 1 = Speed, 2 = Shield
    private int powerUpID;
    [SerializeField]
    private AudioClip _powerupSound;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if (transform.position.y >= 6.50f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
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
                        player.ApplyDamage(60);
                            break;
                    default:
                        Debug.Log("Default Value");
                        break;

                }
            }

            Destroy(this.gameObject);
        }

    }
}
