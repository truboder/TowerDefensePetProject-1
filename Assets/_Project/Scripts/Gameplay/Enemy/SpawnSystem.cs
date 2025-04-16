using System.Collections;
using UnityEngine;
using Zenject;

namespace Game.Enemy
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

            _pool = new ComponentPool<Enemy>(_spawnSettings.DefaultEnemyPrefab, _container);
        }

        public void Initialize()
        {
            PrewarmPool();
            StartWaveSpawning();
        }

        private void PrewarmPool()
        {
            for (int i = 0; i < _spawnSettings.InitialPoolSize; i++)
            {
                Enemy enemy = _pool.Get();
                _pool.Return(enemy);
            }
        }

        private void StartWaveSpawning()
        {
            _coroutineRunner.StartCoroutine(WaveSpawner());
        }

        private IEnumerator WaveSpawner()
        {
            while (_spawnSettings.InfiniteWaves || _currentWave < _spawnSettings.Waves.Count)
            {
                var wave = GetCurrentWave();

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

            var blockChainHandler = enemy.GetComponent<BlockChainHandler>();

            //var movement = enemy.GetComponent<EnemyMovement>();

            //movement.Cleanup();
            //movement.Initialize();

            //movement.PathCompleted += OnPathCompleted;

            if (blockChainHandler == null)
            {
                Debug.LogError("Enemy prefab is missing EnemyBlockChainHandler!");
                return;
            }

            var path = _levelDataSaervice.GetEnemyPath();
            var waypoints = path.GetWaypointsPositions();

            blockChainHandler.Setup(waypoints, _pool, enemy);


            _spawnedCount++;
        }

        //private void OnPathCompleted(EnemyMovement movement)
        //{
        //    movement.PathCompleted -= OnPathCompleted;
        //    movement.Cleanup();
        //    ReturnEnemy(movement.GetComponent<Enemy>());
        //}

        private void ReturnEnemy(Enemy enemy)
        {
            enemy.gameObject.SetActive(false);
            _pool.Return(enemy);
            _spawnedCount--;
        }
    }
}