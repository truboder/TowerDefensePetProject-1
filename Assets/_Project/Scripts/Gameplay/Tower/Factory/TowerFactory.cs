using Gameplay.Enemies;
using Gameplay.Tower.Projectiles;
using Gameplay.Tower.States;
using Gameplay.Tower.StaticData;
using UnityEngine;
using Zenject;

namespace Gameplay.Tower.Factory
{
    public class TowerFactory
    {
        private readonly TowerSettings _settings;
        private readonly ProjectileFactory _projectileFactory;
        private readonly DiContainer _container;
        private readonly TowerSystem _towerSystem;

        public TowerFactory(TowerSettings settings, ProjectileFactory projectileFactory, DiContainer container, TowerSystem towerSystem)
        {
            _settings = settings;
            _projectileFactory = projectileFactory;
            _container = container;
            _towerSystem = towerSystem;
        }

        public Tower Create(Vector3 position)
        {
            var tower = _container.InstantiatePrefabForComponent<Tower>(_settings.TowerPrefab);
            tower.transform.position = position;

            var blackboard = new Blackboard();
            var stateMachine = new TowerStateMachine();
            stateMachine.AddState(new TargetSelectionState(stateMachine, blackboard, tower.gameObject, _settings));
            stateMachine.AddState(new AttackState(stateMachine, blackboard, tower.gameObject, _projectileFactory, _settings));
            tower.Initialize(stateMachine, blackboard);
            stateMachine.SetState<TargetSelectionState>();

            _towerSystem.RegisterTower(tower);
            return tower;
        }
    }
}