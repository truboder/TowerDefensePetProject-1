using AdvertisementSystem;
using AdvertisementSystem.Static_Data;
using Gameplay.Enemies;
using Gameplay.Enemies.Factory;
using Gameplay.Enemies.Static_Data;
using Gameplay.Inputs;
using Gameplay.Levels;
using Gameplay.Scoring;
using Gameplay.Tower;
using Gameplay.Tower.Factory;
using Gameplay.Tower.Projectiles.Factory;
using Gameplay.Tower.StaticData;
using Gameplay.PlayerCastle;
using Gameplay.PlayerCastle.Factory;
using Gameplay.PlayerCastle.StaticData;
using Gameplay.PlayerCastle.UI;
using Gameplay.Scoring.UI;
using Gameplay.Tower.UI;
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
        [SerializeField] private AdsSettings _adsSettings;
        [SerializeField] private CastleHealthUI _castleHealthUI;
        [SerializeField] private ScoreUI _scoreUI;
        [SerializeField] private BuildTowerUI _buildTowerUI;
        [SerializeField] private AdButtonHandler _adButtonHandler;

        public override void InstallBindings()
        {
            BindLevelServices();
            BindCastleServices();
            BindEnemyFeature();
            BindTowerFeature();
            BindScoringFeature();
            BindInputFeature();
            BindAdsFeature();
        }

        private void BindLevelServices()
        {
            Container.BindInterfacesAndSelfTo<LevelDataService>().AsSingle().NonLazy();
        }

        private void BindCastleServices()
        {
            Container.BindInterfacesAndSelfTo<CastleSettings>().FromInstance(_castleSettings).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CastleFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CastleSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CastleHealthUI>().FromInstance(_castleHealthUI).AsSingle().NonLazy();
        }

        private void BindEnemyFeature()
        {
            Container.BindInterfacesAndSelfTo<SpawnSettings>().FromInstance(_enemySpawnSettings).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SpawnSystem>().AsSingle().NonLazy();
        }

        private void BindTowerFeature()
        {
            Container.BindInterfacesAndSelfTo<TowerSettings>().FromInstance(_towerSettings).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ProjectileFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TowerSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TowerFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BuildTowerUI>().FromInstance(_buildTowerUI).AsSingle().NonLazy();
        }

        private void BindScoringFeature()
        {
            Container.BindInterfacesAndSelfTo<ScoreSettings>().FromInstance(_scoreSettings).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ScoreService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ScoreUI>().FromInstance(_scoreUI).AsSingle().NonLazy();
        }

        private void BindInputFeature()
        {
            Container.BindInterfacesAndSelfTo<CameraService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ClickSystem>().AsSingle().NonLazy();
        }

        private void BindAdsFeature()
        {
            Container.BindInterfacesAndSelfTo<AdsSettings>().FromInstance(_adsSettings).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AdsService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AdButtonHandler>().FromInstance(_adButtonHandler).AsSingle().NonLazy();
        }
    }
}