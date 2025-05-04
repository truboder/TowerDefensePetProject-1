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

        private void Update()
        {
            if (_hasTower)
            {
                return;
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        Debug.Log($"Clicked on {gameObject.name}");
                        _towerFactory.Create(transform.position);
                        _hasTower = true;
                    }
                }
            }
        }
    }
}