using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Health : MonoBehaviour
{

    public int MaxHealth;
    public int CurrentHealth;
    public float CurrentHealthPercentage { get { return (float) CurrentHealth / (float) MaxHealth; } }

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void ApplyDamage(int damage)
    {
        CurrentHealth -= damage;

    }
    public void AddHealth(int health)
    {
        CurrentHealth += health;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }
}
