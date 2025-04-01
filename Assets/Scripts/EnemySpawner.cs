using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("General settings")]
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform _spawnPoint;

    [Header("Pool settings")]
    [SerializeField] private int _initialPoolSize = 10;
    [SerializeField] private int _maxPoolSize = 15;

    [Header("Spawn settings")]
    [SerializeField] private int _wavesCount = 5;
    [SerializeField] private int _enemiesPerWave = 5;
    [SerializeField] private float _delayBetweenWaves = 3f;
    [SerializeField] private float _spawnDelayInWave = 1f;
    [SerializeField] private int _totalEnemiesToSpawn = 20;
    [SerializeField] private bool _infiniteWaves = false;

    private ComponentPool<Enemy> _pool;
    private int _spawnedCount = 0;
    private int _currentWave = 0;

    private void Awake()
    {
        if (_enemyPrefab == null)
        {
            return;
        }

        _pool = new ComponentPool<Enemy>(_enemyPrefab, transform);
        PrewarmPool();
    }

    private void Start()
    {
        StartCoroutine(WaveSpawner());
    }

    private void PrewarmPool()
    {
        for (int i = 0; i < _initialPoolSize; i++)
        {
            Enemy enemy = _pool.Get();
            _pool.Return(enemy);
        }
    }

    private IEnumerator WaveSpawner()
    {
        while (_infiniteWaves || _currentWave < _wavesCount)
        {
            _currentWave++;

            for (int i = 0; i < _enemiesPerWave; i++)
            {
                if (_spawnedCount >= _maxPoolSize) break;

                SpawnSingleEnemy();
                yield return new WaitForSeconds(_spawnDelayInWave);
            }

            if (!_infiniteWaves && _currentWave < _wavesCount)
            {
                yield return new WaitForSeconds(_delayBetweenWaves);
            }
            else if (_infiniteWaves)
            {
                yield return new WaitForSeconds(_delayBetweenWaves);
            }
        }
    }

    private void SpawnSingleEnemy()
    {
        if (_spawnedCount >= _maxPoolSize) return;

        var enemy = _pool.Get();
        enemy.transform.position = _spawnPoint != null ? _spawnPoint.position : transform.position;
        _spawnedCount++;
    }

    public void ReturnEnemy(Enemy enemy)
    {
        if (enemy == null) return;

        _pool.Return(enemy);
        _spawnedCount--;
    }
}
