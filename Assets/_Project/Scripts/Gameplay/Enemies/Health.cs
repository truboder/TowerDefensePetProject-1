using System;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class Health
    {
        public int CurrentHealth { get; private set; }
        public bool IsAlive => CurrentHealth > 0;
        public event Action OnDeath;

        public Health(int initialHealth = 10)
        {
            CurrentHealth = initialHealth;
        }

        public void TakeDamage(int amount)
        {
            if (!IsAlive)
            {
                return;
            }

            CurrentHealth -= amount;
            Debug.Log($"Enemy took {amount} damage. Current health: {CurrentHealth}");

            if (CurrentHealth <= 0)
            {
                OnDeath?.Invoke();
            }
        }

        public void ResetHealth(int value)
        {
            CurrentHealth = value;
        }
    }
}