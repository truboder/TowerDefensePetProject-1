using Gameplay.Enemies;
using UnityEngine;

namespace Gameplay.Levels
{
    public interface ILevelDataService
    {
        Vector3 GetSpawnPosition();
        Path GetEnemyPath();
        void SetEnemySpawnPoint(Transform spawnPoint);
        void SetEnemyPath(Path enemyPath);
        void ResetLevelData();
    }
}