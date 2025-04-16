using Game.Enemy;
using UnityEngine;

public interface ILevelDataService
{
    Vector3 GetSpawnPosition();
    Path GetEnemyPath();
    void SetEnemySpawnPoint(Transform spawnPoint);
    void SetEnemyPath(Path enemyPath);
    void ResetLevelData();
}