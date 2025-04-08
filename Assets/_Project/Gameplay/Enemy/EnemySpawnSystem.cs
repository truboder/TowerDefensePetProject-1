using System.Collections;
using UnityEngine;

public class EnemySpawnSystem
{
    private readonly EnemySpawnSettings _spawnSettings;
    private readonly ICoroutineRunService _coroutineRunner;
    private readonly ILevelDataService _levelData;
    private readonly ComponentPool<Enemy> _pool;

    private int _spawnedCount = 0;
    private int _currentWave = 0;

    public EnemySpawnSystem(EnemySpawnSettings spawnSettings)
    {
        _spawnSettings = spawnSettings;
        _coroutineRunner = Container.Instance.Get<ICoroutineRunService>();
        _levelData = Container.Instance.Get<ILevelDataService>();

        if (_spawnSettings == null || _spawnSettings.DefaultEnemyPrefab == null)
        {
            return;
        }

        _pool = new ComponentPool<Enemy>(_spawnSettings.DefaultEnemyPrefab);
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

    private EnemyWaveConfig GetCurrentWave()
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
        enemy.transform.position = _levelData.GetSpawnPosition();

        var movement = enemy.GetComponent<EnemyMovement>();

        movement.Cleanup();
        movement.Initialize();

        movement.OnPathCompleted += ReturnEnemyToPool;

        _spawnedCount++;
    }

    private void ReturnEnemyToPool(EnemyMovement movement)
    {
        movement.OnPathCompleted -= ReturnEnemyToPool;
        movement.Cleanup();
        ReturnEnemy(movement.GetComponent<Enemy>());
    }

    private void ReturnEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        _pool.Return(enemy);
        _spawnedCount--;
    }
}
