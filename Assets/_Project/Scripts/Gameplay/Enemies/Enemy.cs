using System;
using Gameplay.Levels;
using Gameplay.Scoring;
using Gameplay.Enemies.States;
using Gameplay.Enemies.Static_Data;
using Gameplay.HealthSystem;
using UnityEngine;
using Zenject;

namespace Gameplay.Enemies
{
    public class Enemy : MonoBehaviour
    {
        private EnemyStateMachine _stateMachine;
        private Blackboard _blackboard;
        private Health _health;
        private IScoreService _scoreService;
        private ILevelDataService _levelDataService;
        private EnemyType _enemyType = EnemyType.DefaultEnemy;

        public event Action OnPathCompletedEvent;
        public Health Health => _health;
        public EnemyType EnemyType => _enemyType;

        [Inject]
        public void Construct(IScoreService scoreService, ILevelDataService levelDataService)
        {
            _scoreService = scoreService;
            _levelDataService = levelDataService;
        }

        public void Initialize(EnemyStateMachine stateMachine, Blackboard blackboard, Health health)
        {
            _blackboard = blackboard;
            _stateMachine = stateMachine;
            _health = health;

            _stateMachine.OnStateChanged += HandleStateChanged;
            _health.OnDeath += HandleDeath;
        }

        public void SetEnemyType(EnemyType enemyType)
        {
            _enemyType = enemyType;
        }

        private void HandleStateChanged(Type stateType)
        {
            if (stateType == typeof(CompleteState))
            {
                OnPath_COMPLETED();
            }
        }

        private void HandleDeath()
        {
            _scoreService.AddScore(_enemyType);
            OnPath_COMPLETED();
        }

        private void Update()
        {
            _stateMachine?.Update();
        }

        private void OnDestroy()
        {
            _stateMachine?.Dispose();
            _stateMachine.OnStateChanged -= HandleStateChanged;
            _health.OnDeath -= HandleDeath;
        }

        public void OnPath_COMPLETED()
        {
            OnPathCompletedEvent?.Invoke();
        }
    }
}