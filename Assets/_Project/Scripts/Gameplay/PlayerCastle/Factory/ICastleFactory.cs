using UnityEngine;

namespace Gameplay.PlayerCastle.Factory
{
    public interface ICastleFactory
    {
        Castle Create(Vector3 position);
        void Return(Castle castle);
    }
}