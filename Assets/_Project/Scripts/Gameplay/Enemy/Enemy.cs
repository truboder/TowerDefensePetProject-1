using System;
using System.Collections.Generic;
using Gameplay.Enemy.States;
using Gameplay.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.Enemy
{
    public class Enemy : MonoBehaviour
    {
        private EnemyStateMachine _stateMachine;
        private Blackboard _blackboard;

        public event Action OnPathCompletedEvent;

        [Inject]
        public void Construct(HealthService healthService)
        {

        }

        public void Initialize(EnemyStateMachine stateMachine, Blackboard blackboard)
        {
            _blackboard = blackboard;
            _stateMachine = stateMachine;
            
            _stateMachine.OnStateChanged += HandleStateChanged;
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
        }

        public void OnPathCompleted()
        {
            OnPathCompletedEvent?.Invoke();
        }
    }
}