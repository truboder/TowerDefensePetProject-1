using UnityEngine;
using Zenject;

public class EnemyCompleteBlock : EnemyBehaviourBlock
{
    private Enemy _enemy;
    private ComponentPool<Enemy> _pool;

    public void Initialize(Enemy enemy, ComponentPool<Enemy> pool)
    {
        _enemy = enemy;
        _pool = pool;
    }

    public override void Run()
    {
        gameObject.SetActive(false);
    }
}
