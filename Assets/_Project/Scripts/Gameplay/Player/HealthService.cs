using UnityEngine;

namespace _Project.Scripts.Gameplay.Player
{
    public class HealthService
    {
        public int Health { get; private set; } = 10;

        public void TakeDamage(int amount)
        {
            Health -= amount;
            Debug.Log($"Player took {amount} damage. Current health: {Health}");

            if (Health <= 0)
            {
                Debug.Log("Player is dead!");
            }
        }

        public void ResetHealth(int value = 10)
        {
            Health = value;
        }
    }
}
