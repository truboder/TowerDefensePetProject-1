using Gameplay.Tower.Factory;
using UnityEngine;
using Zenject;

namespace Gameplay.Tower
{
    public class TowerPlatform : MonoBehaviour
    {
        private TowerFactory _towerFactory;
        private bool _hasTower = false;

        [Inject]
        public void Construct(TowerFactory towerFactory)
        {
            _towerFactory = towerFactory;
        }

        public void HandleClick()
        {
            if (_hasTower)
            {
                return;
            }
            
            _towerFactory.Create(transform.position);
            _hasTower = true;
        }
    }
}