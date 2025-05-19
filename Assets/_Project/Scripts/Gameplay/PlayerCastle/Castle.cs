using Gameplay.HealthSystem;
using Gameplay.Scoring;
using UnityEngine;
using Zenject;

namespace Gameplay.PlayerCastle
{
    public class Castle : MonoBehaviour
    {
        private Health _health;
        private IScoreService _scoreService;

        public Health Health => _health;

        [Inject]
        public void Construct(IScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        public void Initialize(Health health)
        {
            _health = health;
            _health.OnDeath += HandleDeath;
        }

        private void HandleDeath()
        {
            Debug.Log("Castle: Castle is destroyed!");
        }

        private void OnDestroy()
        {
            _health.OnDeath -= HandleDeath;
        }
    }
}