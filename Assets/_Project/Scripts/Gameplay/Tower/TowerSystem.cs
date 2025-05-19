using System.Collections.Generic;
using Gameplay.Tower.Projectiles.Factory;
using Gameplay.Tower.StaticData;
using Zenject;

namespace Gameplay.Tower
{
    public class TowerSystem : ITickable
    {
        private readonly List<Tower> _towers = new List<Tower>();
        private readonly IProjectileFactory _projectileFactory;
        private readonly TowerSettings _towerSettings;

        public TowerSystem(IProjectileFactory projectileFactory, TowerSettings towerSettings)
        {
            _projectileFactory = projectileFactory;
            _towerSettings = towerSettings;
        }

        public void RegisterTower(Tower tower)
        {
            if (_towers.Contains(tower))
            {
                return;
            }
            
            _towers.Add(tower);
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
    }
}