using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawnSettings _spawnSettings;

    private ComponentPool<Enemy> _pool;
    private int _spawnedCount = 0;
    private int _currentWave = 0;

    private void Awake()
    {
        if (_spawnSettings == null || _spawnSettings.DefaultEnemyPrefab == null)
        {
            return;
        }

        _pool = new ComponentPool<Enemy>(_spawnSettings.DefaultEnemyPrefab, transform);
        PrewarmPool();
    }

    private void Start()
    {
        StartCoroutine(WaveSpawner());
    }

    private void PrewarmPool()
    {
        for (int i = 0; i < _spawnSettings.InitialPoolSize; i++)
        {
            Enemy enemy = _pool.Get();
            _pool.Return(enemy);
        }
    }

    private IEnumerator WaveSpawner()
    {
        while (_spawnSettings.InfiniteWaves|| _currentWave < _spawnSettings.Waves.Count)
        {
            var wave = GetCurrentWave();           

            for (int i = 0; i < wave.EnemyCount; i++)
            {
                if (_spawnedCount >= _spawnSettings.MaxPoolSize) break;

                SpawnSingleEnemy();
                yield return new WaitForSeconds(wave.SpawnInterval);
            }

            if (!ShouldSpawnNextWave()) yield break;

            yield return new WaitForSeconds(wave.DelayAfterWave);
            _currentWave++;
        }
    }

    private EnemyWaveConfig GetCurrentWave()
    {
        if (_spawnSettings.InfiniteWaves)
            return _spawnSettings.Waves[1];

        return _spawnSettings.Waves[_currentWave];
    }

    private bool ShouldSpawnNextWave()
    {
        return _spawnSettings.InfiniteWaves || _currentWave < _spawnSettings.Waves.Count - 1;
    }

    private void SpawnSingleEnemy()
    {
        Enemy enemy = _pool.Get();
        _spawnedCount++;
    }

    public void ReturnEnemy(Enemy enemy)
    {
        if (enemy == null) return;

        _pool.Return(enemy);
        _spawnedCount--;
    }
}