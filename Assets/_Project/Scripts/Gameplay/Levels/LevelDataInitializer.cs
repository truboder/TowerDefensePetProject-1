using Game.Enemy;
using UnityEngine;
using Zenject;

public class LevelDataInitializer : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Path _enemyPath;

    private ILevelDataService _levelDataService;

    [Inject]
    public void Construct(ILevelDataService levelDataService)
    {
        _levelDataService = levelDataService;
        levelDataService.SetEnemySpawnPoint(_spawnPoint);
        levelDataService.SetEnemyPath(_enemyPath);
    }

    private void OnDestroy()
    {
        _levelDataService?.ResetLevelData();
    }
}