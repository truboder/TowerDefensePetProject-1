using System;
using UnityEngine;

namespace Gameplay.Player
{
    public class HealthService
    {
        private readonly int _maxHealth;
        public int Health { get; private set; }
        public bool IsAlive => Health > 0;

        public event Action<int> OnHealthChanged;
        public event Action OnDamageTaken;

        public HealthService(int maxHealth = 10)
        {
            _maxHealth = maxHealth;
            Health = maxHealth;
            Debug.Log($"HealthService initialized with {Health}/{_maxHealth} health");
        }

        public void TakeDamage(int amount)
        {
            if (!IsAlive)
            {
                Debug.Log("HealthService: Player is already dead!");
                return;
            }

            Health = Mathf.Max(0, Health - amount);
            Debug.Log($"HealthService: Player took {amount} damage. Current health: {Health}/{_maxHealth}");

            OnHealthChanged?.Invoke(Health);
            OnDamageTaken?.Invoke();

            if (Health <= 0)
            {
                Debug.Log("HealthService: Player is dead!");
            }
        }

        public void ResetHealth(int value)
        {
            Health = Mathf.Min(value, _maxHealth);
            OnHealthChanged?.Invoke(Health);
            Debug.Log($"HealthService: Health reset to {Health}/{_maxHealth}");
        }
    }
}