using UnityEngine;

public interface ILevelDataService
{
    Vector3 GetSpawnPosition();
    EnemyPath GetEnemyPath();
}
