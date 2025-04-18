using _Project.Scripts.Gameplay.Enemy;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Levels
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