using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private EnemySpawnSettings _enemySpawnSettings;

    public override void InstallBindings()
    {
        Container.Bind<ICoroutineRunService>().To<CoroutineRunService>().AsSingle().NonLazy();
        Container.Bind<ILevelDataService>().To<LevelDataService>().AsSingle().NonLazy();

        Container.Bind<EnemySpawnSettings>().FromInstance(_enemySpawnSettings).AsSingle().NonLazy();
        Container.Bind<EnemySpawnSystem>().AsSingle().NonLazy();
    }
}
