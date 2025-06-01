using System.Collections.Generic;
using Common;
using Gameplay.Enemies.States;
using Gameplay.Enemies.Static_Data;
using Gameplay.Enemies.UI;
using Gameplay.Enemies.UI.Factory;
using Gameplay.HealthSystem;
using Gameplay.Levels;
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
        private readonly EnemyHealthBarFactory _healthBarFactory;

        public EnemyFactory(DiContainer container, SpawnSettings spawnSettings, ILevelDataService levelDataService, EnemyHealthBarFactory healthBarFactory)
        {
            _container = container;
            _spawnSettings = spawnSettings;
            _levelDataService = levelDataService;
            _healthBarFactory = healthBarFactory;
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
            stateMachine.AddState(new AttackState(stateMachine, blackboard, enemy.gameObject, _levelDataService));
            stateMachine.AddState(new CompleteState(stateMachine, blackboard, enemy.gameObject));
            stateMachine.SetState<MoveState>();

            enemy.Initialize(stateMachine, blackboard, new Health());
            enemy.SetEnemyType(enemyType);
            
            EnemyHealthBar healthBar = _healthBarFactory.Create(enemy);
            enemy.OnPathCompletedEvent += () => Return(enemy, healthBar);

            return enemy;
        }

        private void Return(Enemy enemy, EnemyHealthBar healthBar)
        {
            enemy.OnPathCompletedEvent -= () => Return(enemy, healthBar);
            _healthBarFactory.Return(healthBar);
            _pool.Return(enemy);
        }
    }
}