using System;
using UnityEngine;
using Zenject;
using _Project.Scripts.Gameplay.Enemy.States;
using _Project.Scripts.Gameplay.Player;

namespace _Project.Scripts.Gameplay.Enemy
{
    public class Enemy : MonoBehaviour
    {
        private EnemyStateMachine _stateMachine;
        private Blackboard _blackboard;
        private HealthService _healthService;

        public event Action OnPathCompletedEvent;

        [Inject]
        public void Construct(HealthService healthService)
        {
            _healthService = healthService;
        }

        public void Initialize(System.Collections.Generic.List<Vector3> waypoints)
        {
            _blackboard = new Blackboard();
            _blackboard.TrySetData("Waypoints", waypoints);

            _stateMachine = new EnemyStateMachine();
            _stateMachine.AddState(new MoveState(_stateMachine, _blackboard, this));
            _stateMachine.AddState(new AttackState(_stateMachine, _blackboard, this, _healthService));
            _stateMachine.AddState(new CompleteState(_stateMachine, _blackboard, this));
            _stateMachine.SetState<MoveState>();
        }

        private void Update()
        {
            _stateMachine?.Update();
        }

        private void OnDestroy()
        {
            _stateMachine?.Dispose();
        }

        public void OnPathCompleted()
        {
            OnPathCompletedEvent?.Invoke();
        }
    }
}