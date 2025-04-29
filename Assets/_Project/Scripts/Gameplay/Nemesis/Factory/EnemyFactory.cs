using System.Collections.Generic;
using Gameplay.Levels;
using Gameplay.Nemesis.States;
using Gameplay.Nemesis.Static_Data;
using Gameplay.Player;
using UnityEngine;
using Utils;
using Zenject;

namespace Gameplay.Nemesis.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly ComponentPool<Enemy> _pool;
        private readonly SpawnSettings _spawnSettings;
        private readonly ILevelDataService _levelDataService;
        private readonly DiContainer _container;
        private readonly HealthService _healthService;

        public EnemyFactory(SpawnSettings spawnSettings, ILevelDataService levelDataService, DiContainer container, HealthService healthService)
        {
            _spawnSettings = spawnSettings;
            _levelDataService = levelDataService;
            _container = container;
            _healthService = healthService;
            _pool = new ComponentPool<Enemy>(_spawnSettings.DefaultEnemyPrefab, _container);
        }

        public Enemy Create()
        {
            Enemy enemy = _pool.Get();
            enemy.transform.position = _levelDataService.GetSpawnPosition();

            Path path = _levelDataService.GetEnemyPath();
            List<Vector3> waypoints = path.GetWaypointsPositions();
            
            Blackboard blackboard = new Blackboard();
            blackboard.TrySetData("Waypoints", waypoints);

            EnemyStateMachine stateMachine = new EnemyStateMachine();
            stateMachine.AddState(new MoveState(stateMachine, blackboard, enemy));
            stateMachine.AddState(new AttackState(stateMachine, blackboard, enemy, _healthService));
            stateMachine.AddState(new CompleteState(stateMachine, blackboard, enemy));
            stateMachine.SetState<MoveState>();

            enemy.Initialize(stateMachine, blackboard);
            enemy.OnPathCompletedEvent += () => Return(enemy);
            enemy.OnDestroyed += () => Return(enemy);

            return enemy;
        }

        private void Return(Enemy enemy)
        {
            enemy.OnPathCompletedEvent -= () => Return(enemy);
            enemy.OnDestroyed -= () => Return(enemy);
            _pool.Return(enemy);
        }
    }
}