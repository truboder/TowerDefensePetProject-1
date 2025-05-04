using Gameplay.Enemies;
using Gameplay.Enemies.Factory;
using Gameplay.Enemies.Static_Data;
using Gameplay.Levels;
using Gameplay.Player;
using Gameplay.Tower;
using Gameplay.Tower.Factory;
using Gameplay.Tower.Projectiles;
using Gameplay.Tower.StaticData;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameplayLevelInstaller : MonoInstaller
    {
        [SerializeField] private SpawnSettings _enemySpawnSettings;
        [SerializeField] private TowerSettings _towerSettings;

        public override void InstallBindings()
        {
            Container.Bind<ILevelDataService>().To<LevelDataService>().AsSingle().NonLazy();
            Container.Bind<HealthService>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<SpawnSettings>().FromInstance(_enemySpawnSettings).AsSingle().NonLazy();
            Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SpawnSystem>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<TowerSettings>().FromInstance(_towerSettings).AsSingle().NonLazy();
            Container.Bind<ProjectileFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TowerSystem>().AsSingle().NonLazy();
            Container.Bind<TowerFactory>().AsSingle().NonLazy();
        }
    }
}