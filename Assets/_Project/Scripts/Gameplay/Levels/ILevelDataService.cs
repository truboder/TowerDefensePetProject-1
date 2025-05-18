using Gameplay.PlayerCastle;
using Gameplay.Enemies;
using UnityEngine;

namespace Gameplay.Levels
{
    public interface ILevelDataService
    {
        Vector3 GetSpawnPosition();
        Path GetEnemyPath();
        Castle GetCastle(); 
        void SetEnemySpawnPoint(Transform spawnPoint);
        void SetEnemyPath(Path enemyPath);
        void SetCastle(Castle castle);
        void ResetLevelData();
    }
}