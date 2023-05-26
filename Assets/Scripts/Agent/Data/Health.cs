using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //Consts
    private const int MinHealth = 0;
    private const int MinHealthRange = MinHealth + 1;
    private const int MaxHealthRange = 3;

    //Values
    [field: SerializeField, Range(MinHealthRange, MaxHealthRange)] public int MaxHealth {  get; private set; }
    public int CurrentHealth { get; private set; }

    //Actions
    public Action OnDie;
    public Action OnHealthChanged;

    private void Start()
    {
        SetHealth(MaxHealth);
    }

    private void SetHealth(int health)
    {
        CurrentHealth = Mathf.Clamp(health, MinHealth, MaxHealth);

        if (CurrentHealth <= 0)
            OnDie?.Invoke();

        OnHealthChanged?.Invoke();
    }

    public void DealDamage(int damage)
    {
        SetHealth(CurrentHealth - damage);
    }
}
