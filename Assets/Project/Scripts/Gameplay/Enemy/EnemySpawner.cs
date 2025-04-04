using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawnSettings _spawnSettings;

    private EnemySpawnSystem _spawnSystem;

    private void Awake()
    {
        if (_spawnSettings == null || _spawnSettings.DefaultEnemyPrefab == null)
        {
            return;
        }

        var coroutineRunner = Container.Instance.Get<ICoroutineRunService>();
        var levelData = Container.Instance.Get<ILevelDataService>();

        _spawnSystem = new EnemySpawnSystem(_spawnSettings, coroutineRunner, levelData);
    }

    public void ReturnEnemy(Enemy enemy)
    {
        _spawnSystem?.ReturnEnemy(enemy);
    }
}