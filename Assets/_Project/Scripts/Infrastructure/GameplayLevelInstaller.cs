using Gameplay.Nemesis;
using Gameplay.Levels;
using Gameplay.Nemesis.Factory;
using Gameplay.Nemesis.Static_Data;
using Gameplay.Player;
using Gameplay.Tower.StaticData;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameplayLevelInstaller : MonoInstaller
    {
        [SerializeField] private SpawnSettings _enemySpawnSettings;
        [SerializeField] private TowerAttackSettings _towerAttackSettings;

        public override void InstallBindings()
        {
            Container.Bind<ILevelDataService>().To<LevelDataService>().AsSingle().NonLazy();
            Container.Bind<HealthService>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<SpawnSettings>().FromInstance(_enemySpawnSettings).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TowerAttackSettings>().FromInstance(_towerAttackSettings).AsSingle().NonLazy();
            Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SpawnSystem>().AsSingle().NonLazy();
        }
    }
}