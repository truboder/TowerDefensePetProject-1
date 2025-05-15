using Gameplay.Enemies;
using Gameplay.Enemies.Factory;
using Gameplay.Enemies.Static_Data;
using Gameplay.Inputs;
using Gameplay.Levels;
using Gameplay.Player;
using Gameplay.Scoring;
using Gameplay.Tower;
using Gameplay.Tower.Factory;
using Gameplay.Tower.Projectiles;
using Gameplay.Tower.StaticData;
using Static_Data.UI;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameplayLevelInstaller : MonoInstaller
    {
        [SerializeField] private SpawnSettings _enemySpawnSettings;
        [SerializeField] private TowerSettings _towerSettings;
        [SerializeField] private ScoreSettings _scoreSettings;
        [SerializeField] private PlayerHealthUI _playerHealthUI;
        [SerializeField] private ScoreUI _scoreUI;
        [SerializeField] private BuildTowerUI _buildTowerUI;

        public override void InstallBindings()
        {
            BindLevelServices();
            BindPlayerServices();
            BindEnemyFeature();
            BindTowerFeature();
            BindScoringFeature();
            BindInputFeature();
        }

        private void BindLevelServices()
        {
            Container.Bind<ILevelDataService>().To<LevelDataService>().AsSingle().NonLazy();
        }

        private void BindPlayerServices()
        {
            Container.Bind<HealthService>().AsSingle().NonLazy();
            Container.Bind<PlayerHealthUI>().FromInstance(_playerHealthUI).AsSingle().NonLazy();
        }

        private void BindEnemyFeature()
        {
            Container.BindInterfacesAndSelfTo<SpawnSettings>().FromInstance(_enemySpawnSettings).AsSingle().NonLazy();
            Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SpawnSystem>().AsSingle().NonLazy();
        }

        private void BindTowerFeature()
        {
            Container.BindInterfacesAndSelfTo<TowerSettings>().FromInstance(_towerSettings).AsSingle().NonLazy();
            Container.Bind<ProjectileFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TowerSystem>().AsSingle().NonLazy();
            Container.Bind<TowerFactory>().AsSingle().NonLazy();
            Container.Bind<BuildTowerUI>().FromInstance(_buildTowerUI).AsSingle().NonLazy();
        }

        private void BindScoringFeature()
        {
            Container.BindInterfacesAndSelfTo<ScoreSettings>().FromInstance(_scoreSettings).AsSingle().NonLazy();
            Container.Bind<ScoreService>().AsSingle().NonLazy();
            Container.Bind<ScoreUI>().FromInstance(_scoreUI).AsSingle().NonLazy();
        }

        private void BindInputFeature()
        {
            Container.Bind<ICameraService>().To<CameraService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ClickSystem>().AsSingle().NonLazy();
        }
    }
}