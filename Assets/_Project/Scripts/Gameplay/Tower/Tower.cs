using System;
using System.Collections.Generic;
using Gameplay.Enemies;
using UnityEngine;
using Zenject;

namespace Gameplay.Tower
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private SphereCollider _triggerCollider;
        
        private TowerStateMachine _stateMachine;
        private Blackboard _blackboard;
        private TowerSystem _towerSystem;
        private readonly List<Enemy> _enemiesInRange = new List<Enemy>();

        public event Action<Enemy> OnEnemyEntered;
        public event Action<Enemy> OnEnemyExited;
        
        public Vector3 GetPosition() => transform.position;
        public TowerStateMachine StateMachine => _stateMachine;

        [Inject]
        public void Construct(TowerSystem towerSystem)
        {
            _towerSystem = towerSystem;
            _towerSystem.RegisterTower(this);
        }

        public void Initialize(TowerStateMachine stateMachine, Blackboard blackboard)
        {
            _stateMachine = stateMachine;
            _blackboard = blackboard;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Enemy>(out var enemy))
            {
                Debug.Log($"Enemy {enemy.name} entered tower range");
                _enemiesInRange.Add(enemy);
                _blackboard.TrySetData("EnemiesInRange", _enemiesInRange);
                OnEnemyEntered?.Invoke(enemy);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Enemy>(out var enemy))
            {
                _enemiesInRange.Remove(enemy);
                _blackboard.TrySetData("EnemiesInRange", _enemiesInRange);
                OnEnemyExited?.Invoke(enemy);
            }
        }

        private void OnDestroy()
        {
            _towerSystem.UnregisterTower(this);
            _stateMachine?.Dispose();
        }
        
        private void OnDrawGizmos()
        {
            if (_triggerCollider != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, _triggerCollider.radius);
            }
        }
    }
}