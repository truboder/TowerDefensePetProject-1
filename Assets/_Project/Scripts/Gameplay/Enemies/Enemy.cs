using System;
using Gameplay.Enemies.States;
using UnityEngine;
using Zenject;

namespace Gameplay.Enemies
{
    public class Enemy : MonoBehaviour
    {
        private EnemyStateMachine _stateMachine;
        private Blackboard _blackboard;
        private Health _health;

        public event Action OnPathCompletedEvent;

        public Health Health => _health;

        [Inject]
        public void Construct()
        {
        }

        public void Initialize(EnemyStateMachine stateMachine, Blackboard blackboard, Health health)
        {
            _blackboard = blackboard;
            _stateMachine = stateMachine;
            _health = health;

            _stateMachine.OnStateChanged += HandleStateChanged;
            _health.OnDeath += HandleDeath;
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
            _health.OnDeath -= HandleDeath;
        }

        public void OnPathCompleted()
        {
            OnPathCompletedEvent?.Invoke();
        }
    }
}