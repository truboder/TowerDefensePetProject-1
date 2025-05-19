using Gameplay.Tower.Factory;
using Gameplay.Tower.UI;
using UnityEngine;
using Zenject;

namespace Gameplay.Tower
{
    public class TowerPlatform : MonoBehaviour
    {
        private ITowerFactory _towerFactory;
        private BuildTowerUI _buildTowerUI;
        private bool _hasTower = false;

        [Inject]
        public void Construct(ITowerFactory towerFactory, BuildTowerUI buildTowerUI)
        {
            _towerFactory = towerFactory;
            _buildTowerUI = buildTowerUI;
        }

        public void HandleClick()
        {
            if (_hasTower)
                return;

            _buildTowerUI.Show(this);
        }

        public void BuildTower()
        {
            if (_hasTower)
                return;

            _towerFactory.Create(transform.position);
            _hasTower = true;
        }
    }
}