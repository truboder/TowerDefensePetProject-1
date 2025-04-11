using Zenject;
using UnityEngine;

public class GameplayLevelInstaller : MonoInstaller
{
    [SerializeField] private EnemySpawnSettings _enemySpawnSettings;

    public override void InstallBindings()
    {
        Container.Bind<ILevelDataService>().To<LevelDataService>().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<EnemySpawnSettings>().FromInstance(_enemySpawnSettings).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<EnemySpawnSystem>().AsSingle().NonLazy();
    }
}
