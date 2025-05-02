using System.Collections.Generic;
using Gameplay.Enemies;
using Gameplay.Tower.States;
using Gameplay.Tower.StaticData;
using Zenject;
using AttackState = Gameplay.Tower.States.AttackState;

namespace Gameplay.Tower
{
    public class TowerSystem : ITickable
    {
        private readonly List<Tower> _towers = new List<Tower>();
        private readonly ProjectileFactory.ProjectileFactory _projectileFactory;
        private readonly TowerSettings _towerSettings;

        public TowerSystem(ProjectileFactory.ProjectileFactory projectileFactory, TowerSettings towerSettings)
        {
            _projectileFactory = projectileFactory;
            _towerSettings = towerSettings;
        }

        public void RegisterTower(Tower tower)
        {
            if (!_towers.Contains(tower))
            {
                _towers.Add(tower);
                InitializeTower(tower);
            }
        }

        public void UnregisterTower(Tower tower)
        {
            _towers.Remove(tower);
        }

        public void Tick()
        {
            foreach (var tower in _towers)
            {
                tower.StateMachine?.Update();
            }
        }

        private void InitializeTower(Tower tower)
        {
            var blackboard = new Blackboard();
            var stateMachine = new TowerStateMachine();
            
            stateMachine.AddState(new IdleState(stateMachine, blackboard, tower, _towerSettings));
            stateMachine.AddState(new AttackState(stateMachine, blackboard, tower, _projectileFactory, _towerSettings));
            
            tower.Initialize(stateMachine, blackboard);
            stateMachine.SetState<IdleState>();
        }
    }
}