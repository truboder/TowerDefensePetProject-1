using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataService : MonoBehaviour, ILevelDataService
{
    private Transform _spawnPoint;
    private EnemyPath _enemyPath;

    public void Initialize(Transform spawnPoint, EnemyPath enemyPath)
    {
        _spawnPoint = spawnPoint;
        _enemyPath = enemyPath;
    }

    public Vector3 GetSpawnPosition() => _spawnPoint.position;
    public EnemyPath GetEnemyPath() => _enemyPath;
}
