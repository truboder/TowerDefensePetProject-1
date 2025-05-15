using System;
using Gameplay.Enemies.States;
using Gameplay.Scoring;
using Gameplay.Enemies.Static_Data; // Добавлено для EnemyType
using UnityEngine;
using Zenject;

namespace Gameplay.Enemies
{
    public class Enemy : MonoBehaviour
    {
        private EnemyStateMachine _stateMachine;
        private Blackboard _blackboard;
        private Health _health;
        private ScoreService _scoreService;
        private EnemyType _enemyType = EnemyType.DefaultEnemy;

        public event Action OnPathCompletedEvent;
        public Health Health => _health;
        public EnemyType EnemyType => _enemyType;

        [Inject]
        public void Construct(ScoreService scoreService)
        {
            _scoreService = scoreService;
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
                OnPathCompleted();
            }
        }

        private void HandleDeath()
        {
            _scoreService.AddScore(_enemyType); // Изменено на передачу enum
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
            _health.OnDeath -= HandleDeath;
        }

        public void OnPathCompleted()
        {
            OnPathCompletedEvent?.Invoke();
        }
    }
}