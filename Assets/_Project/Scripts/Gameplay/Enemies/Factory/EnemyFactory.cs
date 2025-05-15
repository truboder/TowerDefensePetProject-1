using System.Collections.Generic;
using Gameplay.Enemies.States;
using Gameplay.Enemies.Static_Data;
using Gameplay.Levels;
using Gameplay.Player;
using UnityEngine;
using Utils;
using Zenject;

namespace Gameplay.Enemies.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private const string WaypointsKey = "Waypoints";
        
        private readonly ComponentPool<Enemy> _pool;
        private readonly SpawnSettings _spawnSettings;
        private readonly ILevelDataService _levelDataService;
        private readonly DiContainer _container;
        private readonly HealthService _playerHealthService;

        public EnemyFactory(DiContainer container, SpawnSettings spawnSettings, ILevelDataService levelDataService, 
            HealthService playerHealthService)
        {
            _container = container;
            _spawnSettings = spawnSettings;
            _levelDataService = levelDataService;
            _playerHealthService = playerHealthService;
            _pool = new ComponentPool<Enemy>(_spawnSettings.DefaultEnemyPrefab, _container);
        }

        public Enemy Create(EnemyType enemyType)
        {
            Enemy enemy = _pool.Get();
            enemy.transform.position = _levelDataService.GetSpawnPosition();

            Path path = _levelDataService.GetEnemyPath();
            List<Vector3> waypoints = path.GetWaypointsPositions();
            
            Blackboard blackboard = new Blackboard();
            blackboard.TrySetData(WaypointsKey, waypoints);

            EnemyStateMachine stateMachine = new EnemyStateMachine();
            stateMachine.AddState(new MoveState(stateMachine, blackboard, enemy.gameObject));
            stateMachine.AddState(new AttackState(stateMachine, blackboard, enemy.gameObject, _playerHealthService));
            stateMachine.AddState(new CompleteState(stateMachine, blackboard, enemy.gameObject));
            stateMachine.SetState<MoveState>();

            enemy.Initialize(stateMachine, blackboard, new Health());
            enemy.SetEnemyType(enemyType); // Устанавливаем тип врага
            enemy.OnPathCompletedEvent += () => Return(enemy);

            return enemy;
        }

        private void Return(Enemy enemy)
        {
            enemy.OnPathCompletedEvent -= () => Return(enemy);
            _pool.Return(enemy);
        }
    }
}