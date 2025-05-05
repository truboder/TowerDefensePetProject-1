using Gameplay.Tower;
using UnityEngine;
using Zenject;

namespace Gameplay.Inputs
{
    public class ClickSystem : ITickable
    {
        private readonly ICameraService _cameraService;
        private readonly int _layerMask;

        [Inject]
        public ClickSystem(ICameraService cameraService)
        {
            _cameraService = cameraService;
            _layerMask = 1 << LayerMask.NameToLayer("Platform");
        }

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _cameraService.GetRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 10f);

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask))
                {
                    var platform = hit.collider.GetComponent<TowerPlatform>();
                    if (platform != null)
                    {
                        platform.HandleClick();
                    }
                }
            }
        }
    }
}