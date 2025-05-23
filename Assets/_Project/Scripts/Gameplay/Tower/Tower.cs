using System;
using System.Collections.Generic;
using Common;
using Gameplay.Enemies;
using UnityEngine;
using Zenject;

namespace Gameplay.Tower
{
    public class Tower : MonoBehaviour
    {
        private const string EnemiesInRangeKey = "EnemiesInRange";
        
        [SerializeField] private SphereCollider _triggerCollider;
        [SerializeField] private Transform _gun;
        [SerializeField] private Transform _shotPoint;
        
        private TowerStateMachine _stateMachine;
        private Blackboard _blackboard;
        private TowerSystem _towerSystem;
        private readonly List<Enemy> _enemiesInRange = new List<Enemy>();

        public event Action<Enemy> OnEnemyEntered;
        public event Action<Enemy> OnEnemyExited;

        [Inject]
        public void Construct(TowerSystem towerSystem)
        {
            _towerSystem = towerSystem;
        }
        
        public Transform Gun => _gun;
        public Transform ShotPoint => _shotPoint;
        public TowerStateMachine StateMachine => _stateMachine;

        public void Initialize(TowerStateMachine stateMachine, Blackboard blackboard)
        {
            _stateMachine = stateMachine;
            _blackboard = blackboard;
            _blackboard.TrySetData(EnemiesInRangeKey, _enemiesInRange);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<Enemy>(out var enemy)) return;

            _enemiesInRange.Add(enemy);
            OnEnemyEntered?.Invoke(enemy);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<Enemy>(out var enemy))
                return;
            
            _enemiesInRange.Remove(enemy);
            OnEnemyExited?.Invoke(enemy);
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