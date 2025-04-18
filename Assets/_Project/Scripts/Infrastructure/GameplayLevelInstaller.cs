using _Project.Scripts.Gameplay.Enemy;
using _Project.Scripts.Gameplay.Enemy.Static_Data;
using _Project.Scripts.Gameplay.Levels;
using _Project.Scripts.Gameplay.Player;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public class GameplayLevelInstaller : MonoInstaller
    {
        [SerializeField] private SpawnSettings _enemySpawnSettings;

        public override void InstallBindings()
        {
            Container.Bind<ILevelDataService>().To<LevelDataService>().AsSingle().NonLazy();
            Container.Bind<HealthService>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<SpawnSettings>().FromInstance(_enemySpawnSettings).AsSingle().NonLazy();
            Container.Bind<EnemyFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SpawnSystem>().AsSingle().NonLazy();
        }
    }
}