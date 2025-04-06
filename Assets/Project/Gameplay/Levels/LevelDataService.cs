using UnityEngine;

public class LevelDataService : ILevelDataService
{
    private Transform _spawnPoint;
    private EnemyPath _enemyPath;

    public Vector3 GetSpawnPosition() => _spawnPoint?.position ?? Vector3.zero;
    public EnemyPath GetEnemyPath() => _enemyPath;

    public void SetEnemySpawnPoint(Transform spawnPoint) => _spawnPoint = spawnPoint;
    public void SetEnemyPath(EnemyPath enemyPath) => _enemyPath = enemyPath;
    public void ResetLevelData()
    {
        _spawnPoint = null;
        _enemyPath = null;
    }
}