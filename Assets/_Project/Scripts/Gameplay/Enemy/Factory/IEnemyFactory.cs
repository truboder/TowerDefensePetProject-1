using UnityEngine;

namespace Gameplay.Enemy.Factory
{
    public interface IEnemyFactory
    {
        Enemy Create();
    }
}