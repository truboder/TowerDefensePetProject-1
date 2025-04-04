using UnityEngine;

[DefaultExecutionOrder(-1)]
public class Installer : MonoBehaviour
{
    [SerializeField] private EnemyPath _enemyPath;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private EnemySpawnSettings _spawnSettings;

    private void Awake()
    {
        Container.Instance.Register<EnemyPath>(_enemyPath);

        CoroutineRunService coroutineRunner = gameObject.AddComponent<CoroutineRunService>();
        Container.Instance.Register<ICoroutineRunService>(coroutineRunner);

        LevelDataService levelData = gameObject.AddComponent<LevelDataService>();
        levelData.Initialize(_spawnPoint, _enemyPath);
        Container.Instance.Register<ILevelDataService>(levelData);

        EnemySpawnSystem spawnSystem = new EnemySpawnSystem(_spawnSettings, coroutineRunner, levelData);
        Container.Instance.Register<EnemySpawnSystem>(spawnSystem);
    }
}