using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Health : MonoBehaviour
{

    public int MaxHealth;
    public int CurrentHealth;
    private DiverPlayer _diver;
    public float CurrentHealthPercentage { get { return (float) CurrentHealth / (float) MaxHealth; } }

    private void Awake()
    {
        CurrentHealth = MaxHealth;
        _diver = GetComponent<DiverPlayer>();
    }

    public void ApplyDamage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log(gameObject.name + "took damage:" + damage);
        Debug.Log(gameObject.name + "current health: " + CurrentHealth);
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (CurrentHealth <= 0)
        {
            _diver.SwitchStateTo(DiverPlayer.CharacterState.Dead);
        }
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
