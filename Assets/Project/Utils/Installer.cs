using UnityEngine;

[DefaultExecutionOrder(-1)]
public class Installer : MonoBehaviour
{
    [SerializeField] private EnemySpawnSettings _spawnSettings;

    private void Awake()
    {
        var coroutineService = new CoroutineRunService();
        Container.Instance.Register<ICoroutineRunService>(coroutineService);

        var levelDataService = new LevelDataService();
        Container.Instance.Register<ILevelDataService>(levelDataService);

        var spawnSystem = new EnemySpawnSystem(_spawnSettings);
        Container.Instance.Register<EnemySpawnSystem>(spawnSystem);
    }
}
