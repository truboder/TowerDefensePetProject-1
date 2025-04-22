using System.Collections;
using Gameplay.Enemy.Static_Data;
using UnityEngine;
using Utils;
using Zenject;

namespace Gameplay.Enemy
{
    public class SpawnSystem : IInitializable
    {
        private readonly SpawnSettings _spawnSettings;
        private readonly ICoroutineRunService _coroutineRunner;
        private readonly EnemyFactory _enemyFactory;

        private int _spawnedCount = 0;
        private int _currentWave = 0;

        public SpawnSystem(SpawnSettings spawnSettings, ICoroutineRunService coroutineRunService, EnemyFactory enemyFactory)
        {
            _spawnSettings = spawnSettings;
            _coroutineRunner = coroutineRunService;
            _enemyFactory = enemyFactory;
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
            _enemyFactory.Create();
            _spawnedCount++;
        }
    }
}