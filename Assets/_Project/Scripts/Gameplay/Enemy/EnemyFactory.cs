using System.Collections.Generic;
using UnityEngine;
using Zenject;
using _Project.Scripts.Gameplay.Levels;
using _Project.Scripts.Utils;
using Game.Enemy;

namespace _Project.Scripts.Gameplay.Enemy
{
    public class EnemyFactory
    {
        private readonly ComponentPool<Enemy> _pool;
        private readonly SpawnSettings _spawnSettings;
        private readonly ILevelDataService _levelDataService;
        private readonly DiContainer _container;

        public EnemyFactory(SpawnSettings spawnSettings, ILevelDataService levelDataService, DiContainer container)
        {
            _spawnSettings = spawnSettings;
            _levelDataService = levelDataService;
            _container = container;
            _pool = new ComponentPool<Enemy>(_spawnSettings.DefaultEnemyPrefab, _container);
        }

        public Enemy Create()
        {
            Enemy enemy = _pool.Get();
            enemy.transform.position = _levelDataService.GetSpawnPosition();

            Path path = _levelDataService.GetEnemyPath();
            List<Vector3> waypoints = path.GetWaypointsPositions();

            enemy.Initialize(waypoints);
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