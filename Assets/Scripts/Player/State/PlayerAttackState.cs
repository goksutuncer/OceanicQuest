using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : StateBase
{
    public override void EnterActions()
    {
        AimAndFire();
    }

    public override void ExitActions()
    {
    }

    void AimAndFire()
    {
        // Aim the weapon towards the mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Fire the weapon
        Fire();
    }

    void Fire()
    {
        // Instantiate a bullet at the firePoint
        //Instantiate(_weapon, transform.position + new Vector3(1f, 0.5f, 0), Quaternion.identity);
    }
}
