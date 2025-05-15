using Gameplay.Enemies;
using Gameplay.Enemies.Static_Data;

public interface IEnemyFactory
{
    Enemy Create(EnemyType enemyType);
}