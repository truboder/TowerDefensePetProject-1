using System;
using Gameplay.Enemies.States;
using Gameplay.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.Enemies
{
    public class Enemy : MonoBehaviour
    {
        private EnemyStateMachine _stateMachine;
        private Blackboard _blackboard;
        private EnemyHealthService _healthService;

        public event Action OnPathCompletedEvent;

        [Inject]
        public void Construct(HealthService healthService, EnemyHealthService enemyHealthService)
        {
            _healthService = enemyHealthService;
        }

        public void Initialize(EnemyStateMachine stateMachine, Blackboard blackboard, EnemyHealthService healthService)
        {
            _blackboard = blackboard;
            _stateMachine = stateMachine;
            _healthService = healthService;

            _stateMachine.OnStateChanged += HandleStateChanged;
            _healthService.OnDeath += HandleDeath;
        }

        private void HandleStateChanged(Type stateType)
        {
            if (stateType == typeof(CompleteState))
            {
                OnPathCompleted();
            }
        }

        private void HandleDeath()
        {
            OnPathCompleted();
        }

        private void Update()
        {
            _stateMachine?.Update();
        }

        private void OnDestroy()
        {
            _stateMachine?.Dispose();
            _stateMachine.OnStateChanged -= HandleStateChanged;
            _healthService.OnDeath -= HandleDeath;
        }

        public void OnPathCompleted()
        {
            OnPathCompletedEvent?.Invoke();
        }

        public EnemyHealthService HealthService => _healthService;
    }
}