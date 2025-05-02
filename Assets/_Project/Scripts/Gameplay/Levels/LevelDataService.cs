using Gameplay.Enemies;
using UnityEngine;

namespace Gameplay.Levels
{
    public class LevelDataService : ILevelDataService
    {
        private Transform _spawnPoint;
        private Path _enemyPath;

        public Vector3 GetSpawnPosition() => _spawnPoint?.position ?? Vector3.zero;
        public Path GetEnemyPath() => _enemyPath;

        public void SetEnemySpawnPoint(Transform spawnPoint) => _spawnPoint = spawnPoint;
        public void SetEnemyPath(Path enemyPath) => _enemyPath = enemyPath;
        public void ResetLevelData()
        {
            _spawnPoint = null;
            _enemyPath = null;
        }
    }
}