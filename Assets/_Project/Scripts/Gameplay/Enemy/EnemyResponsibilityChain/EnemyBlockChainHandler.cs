using System.Collections.Generic;
using UnityEngine;

public class EnemyBlockChainHandler : MonoBehaviour
{
    [SerializeField] private EnemyMoveBlock _moveBlock;
    [SerializeField] private EnemyAttackBlock _attackBlock;
    [SerializeField] private EnemyCompleteBlock _completeBlock;

    public void Setup(List<Vector3> waypoints, ComponentPool<Enemy> pool, Enemy enemy)
    {
        _moveBlock.SetNext(_attackBlock);
        _attackBlock.SetNext(_completeBlock);

        _moveBlock.InitializePath(waypoints);

        if (_completeBlock is EnemyCompleteBlock completeBlock)
        {
            completeBlock.Initialize(enemy, pool);
        }
    }
}
