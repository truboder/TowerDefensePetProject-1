using Zenject;
using UnityEngine;
using Game.Enemy;
using Game.Player;

public class GameplayLevelInstaller : MonoInstaller
{
    [SerializeField] private SpawnSettings _enemySpawnSettings;

    public override void InstallBindings()
    {
        Container.Bind<ILevelDataService>().To<LevelDataService>().AsSingle().NonLazy();
        Container.Bind<HealthService>().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<SpawnSettings>().FromInstance(_enemySpawnSettings).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<SpawnSystem>().AsSingle().NonLazy();
    }
}