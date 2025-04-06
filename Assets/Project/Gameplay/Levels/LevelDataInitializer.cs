using UnityEngine;

public class LevelDataInitializer : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private EnemyPath _enemyPath;

    private ILevelDataService _levelDataService;

    public void Construct(ILevelDataService levelDataService)
    {
        _levelDataService = levelDataService;
    }

    private void Start()
    {
        if (_levelDataService != null)
        {
            _levelDataService.SetEnemySpawnPoint(_spawnPoint);
            _levelDataService.SetEnemyPath(_enemyPath);
        }
    }

    private void OnDestroy()
    {
        _levelDataService?.ResetLevelData();
    }
}
