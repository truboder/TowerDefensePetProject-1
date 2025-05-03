using Gameplay.Tower.Factory;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Gameplay.Tower
{
    public class TowerPlatform : MonoBehaviour, IPointerClickHandler
    {
        private TowerFactory _towerFactory;
        private bool _hasTower;

        [Inject]
        public void Construct(TowerFactory towerFactory)
        {
            _towerFactory = towerFactory;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_hasTower) return;
            
            _towerFactory.Create(transform.position);
            _hasTower = true;
        }
    }
}