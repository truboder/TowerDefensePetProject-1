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
    [SerializeField] private float _spawnDelay = 1f;
    [SerializeField] private int _totalEnemiesToSpawn = 20;

    private ComponentPool<Enemy> _pool;
    private int _spawnedCount = 0;

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
        StartCoroutine(SpawnEnemiesOverTime());
    }

    private void PrewarmPool()
    {
        for (int i = 0; i < _initialPoolSize; i++)
        {
            Enemy enemy = Instantiate(_enemyPrefab, transform);
            enemy.gameObject.SetActive(false);
            _pool.Return(enemy);
        }
    }

    private IEnumerator SpawnEnemiesOverTime()
    {
        int spawned = 0;

        while (spawned < _totalEnemiesToSpawn && _spawnedCount < _maxPoolSize)
        {
            SpawnSingleEnemy();
            spawned++;
            yield return new WaitForSeconds(_spawnDelay);
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
