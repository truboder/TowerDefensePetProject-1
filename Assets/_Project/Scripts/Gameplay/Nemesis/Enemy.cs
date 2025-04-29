using System;
using Gameplay.Nemesis.States;
using Gameplay.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.Nemesis
{
    public class Enemy : MonoBehaviour
    {
        private EnemyStateMachine _stateMachine;
        private Blackboard _blackboard;
        private int _health = 5;

        public event Action OnPathCompletedEvent;
        public event Action OnDestroyed;

        [Inject]
        public void Construct(HealthService healthService)
        {

        }

        public void Initialize(EnemyStateMachine stateMachine, Blackboard blackboard)
        {
            _blackboard = blackboard;
            _stateMachine = stateMachine;
            
            _stateMachine.OnStateChanged += HandleStateChanged;
            _health = 5;
        }
        
        public void TakeDamage(int damage)
        {
            _health -= damage;
            Debug.Log($"Enemy took {damage} damage. Current health: {_health}");

            if (_health <= 0)
            {
                OnDestroyed?.Invoke();
            }
        }

        private void HandleStateChanged(Type stateType)
        {
            if (stateType == typeof(CompleteState))
            {
                OnPathCompleted();
            }
        }

        private void Update()
        {
            _stateMachine?.Update();
        }

        private void OnDestroy()
        {
            _stateMachine?.Dispose();
            _stateMachine.OnStateChanged -= HandleStateChanged;
            OnDestroyed?.Invoke();
        }

        public void OnPathCompleted()
        {
            OnPathCompletedEvent?.Invoke();
        }
    }
}