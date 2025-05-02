using System;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class EnemyHealthService
    {
        public int Health { get; private set; }
        public bool IsAlive => Health > 0;
        public event Action OnDeath;

        public EnemyHealthService(int initialHealth = 10)
        {
            Health = initialHealth;
        }

        public void TakeDamage(int amount)
        {
            if (!IsAlive) return;

            Health -= amount;
            Debug.Log($"Enemy took {amount} damage. Current health: {Health}");

            if (Health <= 0)
            {
                OnDeath?.Invoke();
            }
        }

        public void ResetHealth(int value)
        {
            Health = value;
        }
    }
}