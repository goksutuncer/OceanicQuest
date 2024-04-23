using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkHealth : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHealth;
    public float CurrentHealthPercentage { get { return (float)CurrentHealth / (float)MaxHealth; } }

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void ApplyDamage(int damage)
    {
        CurrentHealth -= damage;

    }

}
