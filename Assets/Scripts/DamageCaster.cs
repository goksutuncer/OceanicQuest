using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class DamageCaster : MonoBehaviour
{
    private Collider _damageCasterCollider;
    public int Damage = 30;
    public string TargetTag;

    public void Awake()
    {
        _damageCasterCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TargetTag)
        {
            DiverPlayer targetPlayer = other.GetComponent<DiverPlayer>();
            if (targetPlayer != null)
            {
                targetPlayer.ApplyDamage(Damage, transform.parent.position);
                /*PlayerVFXManager playerVFXManager = transform.parent.GetComponent<PlayerVFXManager>();
                if (playerVFXManager != null)
                {
                    RaycastHit hit;
                    Vector3 originalPos = transform.position + (-_damageCasterCollider.bounds.extents.z) * transform.forward;
                    bool isHit = Physics.BoxCast(originalPos, _damageCasterCollider.bounds.extents / 2, transform.forward, out hit, transform.rotation, _damageCasterCollider.bounds.extents.z, 1 << 6);
                    if (isHit)
                    {
                        playerVFXManager.PlaySlash(hit.point + new Vector3(0, 0.5f, 0));
                    }
                }*/
            }
        }
    }
    
}
