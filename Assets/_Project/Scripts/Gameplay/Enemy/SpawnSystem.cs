using System.Collections;
using _Project.Scripts.Gameplay.Levels;
using _Project.Scripts.Utils;
using Game.Enemy;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Enemy
{
    public class SpawnSystem : IInitializable
    {
        private readonly SpawnSettings _spawnSettings;
        private readonly ICoroutineRunService _coroutineRunner;
        private readonly ILevelDataService _levelDataSaervice;
        private readonly ComponentPool<Enemy> _pool;
        private readonly DiContainer _container;

        private int _spawnedCount = 0;
        private int _currentWave = 0;

        public SpawnSystem(SpawnSettings spawnSettings, ICoroutineRunService coroutineRunService, ILevelDataService levelDataService, DiContainer container)
        {
            _spawnSettings = spawnSettings;
            _coroutineRunner = coroutineRunService;
            _levelDataSaervice = levelDataService;
            _container = container;

            _pool = new ComponentPool<_Project.Scripts.Gameplay.Enemy.Enemy>(_spawnSettings.DefaultEnemyPrefab, _container);
        }

        public void Initialize()
        {
            StartWaveSpawning();
        }

        private void StartWaveSpawning()
        {
            _coroutineRunner.StartCoroutine(WaveSpawner());
        }

        private IEnumerator WaveSpawner()
        {
            while (_spawnSettings.InfiniteWaves || _currentWave < _spawnSettings.Waves.Count)
            {
                WaveConfig wave = GetCurrentWave();

                for (int i = 0; i < wave.EnemyCount; i++)
                {
                    if (_spawnedCount >= _spawnSettings.MaxPoolSize) break;

                    SpawnSingleEnemy();
                    yield return new WaitForSeconds(wave.SpawnInterval);
                }

                if (!ShouldSpawnNextWave())
                {
                    yield break;
                }

                yield return new WaitForSeconds(wave.DelayAfterWave);
                _currentWave++;
            }
        }

        private WaveConfig GetCurrentWave()
        {
            if (_spawnSettings.InfiniteWaves)
            {
                return _spawnSettings.Waves[1];
            }

            return _spawnSettings.Waves[_currentWave];
        }

        private bool ShouldSpawnNextWave()
        {
            return _spawnSettings.InfiniteWaves || _currentWave < _spawnSettings.Waves.Count - 1;
        }

        private void SpawnSingleEnemy()
        {
            Enemy enemy = _pool.Get();
            enemy.transform.position = _levelDataSaervice.GetSpawnPosition();

            Path path = _levelDataSaervice.GetEnemyPath();
            var waypoints= path.GetWaypointsPositions();
            
            enemy.Initialize(waypoints);
            enemy.OnPathCompletedEvent += () => OnPathCompleted(enemy);

            _spawnedCount++;
        }

        private void OnPathCompleted(_Project.Scripts.Gameplay.Enemy.Enemy enemy)
        {
            enemy.OnPathCompletedEvent -= () => OnPathCompleted(enemy);
            _pool.Return(enemy);
            _spawnedCount--;
        }
    }
}