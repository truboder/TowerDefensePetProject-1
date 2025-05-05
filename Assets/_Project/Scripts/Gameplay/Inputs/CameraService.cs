using UnityEngine;

namespace Gameplay.Inputs
{
    public class CameraService : ICameraService
    {
        private readonly Camera _mainCamera;

        public CameraService()
        {
            _mainCamera = Camera.main;
        }

        public Ray GetRay(Vector3 screenPosition)
        {
            if (_mainCamera == null)
            {
                return new Ray(Vector3.zero, Vector3.forward);
            }
            return _mainCamera.ScreenPointToRay(screenPosition);
        }
    }
}