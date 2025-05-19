using UnityEngine;

namespace Gameplay.Tower.Factory
{
    public interface ITowerFactory
    {
        Tower Create(Vector3 position);
    }
}