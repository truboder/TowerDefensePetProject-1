using Gameplay.Enemies.Static_Data;

namespace Gameplay.Enemies.Factory
{
    public interface IEnemyFactory
    {
        Enemy Create(EnemyType enemyType);
    }
}