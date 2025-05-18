// Infrastructure/GameplayLevelInstaller.cs
using Gameplay.Enemies;
using Gameplay.Enemies.Factory;
using Gameplay.Enemies.Static_Data;
using Gameplay.Inputs;
using Gameplay.Levels;
using Gameplay.Scoring;
using Gameplay.Tower;
using Gameplay.Tower.Factory;
using Gameplay.Tower.Projectiles;
using Gameplay.Tower.StaticData;
using Gameplay.PlayerCastle;
using Gameplay.PlayerCastle.Factory;
using Gameplay.PlayerCastle.StaticData;
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
        [SerializeField] private CastleSettings _castleSettings;
        [SerializeField] private CastleHealthUI _castleHealthUI;
        [SerializeField] private ScoreUI _scoreUI;
        [SerializeField] private BuildTowerUI _buildTowerUI;

        public override void InstallBindings()
        {
            BindLevelServices();
            BindCastleServices();
            BindEnemyFeature();
            BindTowerFeature();
            BindScoringFeature();
            BindInputFeature();
        }

        private void BindLevelServices()
        {
            Container.Bind<ILevelDataService>().To<LevelDataService>().AsSingle().NonLazy();
        }

        private void BindCastleServices()
        {
            if (_castleSettings == null)
            {
                Debug.LogError("CastleSettings is null in GameplayLevelInstaller!");
            }
            else
            {
                Debug.Log("CastleSettings assigned: " + _castleSettings.name);
            }
            
            Container.BindInterfacesAndSelfTo<CastleSettings>().FromInstance(_castleSettings).AsSingle().NonLazy();
            Container.Bind<ICastleFactory>().To<CastleFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CastleSystem>().AsSingle().NonLazy();
            Container.Bind<CastleHealthUI>().FromInstance(_castleHealthUI).AsSingle().NonLazy();
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