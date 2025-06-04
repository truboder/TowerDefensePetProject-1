using System;
using UnityEngine;

namespace Gameplay.HealthSystem
{
    public class Health
    {
        private readonly int _maxHealth;
        public int CurrentHealth { get; private set; }
        public bool IsAlive => CurrentHealth > 0;
        public int MaxHealth => _maxHealth;

        public event Action<int> OnHealthChanged;
        public event Action OnDamageTaken;
        public event Action OnDeath;

        public Health(int maxHealth = 10)
        {
            _maxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public void TakeDamage(int amount)
        {
            if (!IsAlive)
                return;

            CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
            Debug.Log($"Health: Took {amount} damage. Current health: {CurrentHealth}/{_maxHealth}");

            OnHealthChanged?.Invoke(CurrentHealth);
            OnDamageTaken?.Invoke();

            if (CurrentHealth <= 0)
            {
                OnDeath?.Invoke();
            }
        }

        public void ResetHealth(int value)
        {
            CurrentHealth = Mathf.Min(value, _maxHealth);
            OnHealthChanged?.Invoke(CurrentHealth);
        }
    }
}