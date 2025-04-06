using UnityEngine;

public interface ILevelDataService
{
    Vector3 GetSpawnPosition();
    EnemyPath GetEnemyPath();
    void SetEnemySpawnPoint(Transform spawnPoint);
    void SetEnemyPath(EnemyPath enemyPath);
    void ResetLevelData();
}
