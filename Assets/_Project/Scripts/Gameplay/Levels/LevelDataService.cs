using UnityEngine;

public class LevelDataService : ILevelDataService
{
    private Transform _spawnPoint;
    private Game.Enemy.Path _enemyPath;

    public Vector3 GetSpawnPosition() => _spawnPoint?.position ?? Vector3.zero;
    public Game.Enemy.Path GetEnemyPath() => _enemyPath;

    public void SetEnemySpawnPoint(Transform spawnPoint) => _spawnPoint = spawnPoint;
    public void SetEnemyPath(Game.Enemy.Path enemyPath) => _enemyPath = enemyPath;
    public void ResetLevelData()
    {
        _spawnPoint = null;
        _enemyPath = null;
    }
}