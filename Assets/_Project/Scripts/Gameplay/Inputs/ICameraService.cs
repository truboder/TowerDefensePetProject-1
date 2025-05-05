using UnityEngine;

namespace Gameplay.Inputs
{
    public interface ICameraService
    {
        Ray GetRay(Vector3 screenPosition);
    }
}