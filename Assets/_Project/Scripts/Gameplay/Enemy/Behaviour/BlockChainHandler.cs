using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class BlockChainHandler : MonoBehaviour
    {
        [SerializeField] private MoveBlock _moveBlock;
        [SerializeField] private AttackBlock _attackBlock;
        [SerializeField] private CompleteBlock _completeBlock;

        public void Setup(List<Vector3> waypoints, ComponentPool<Enemy> pool, Enemy enemy)
        {
            _moveBlock.SetNext(_attackBlock);
            _attackBlock.SetNext(_completeBlock);

            _moveBlock.InitializePath(waypoints);

            if (_completeBlock is CompleteBlock completeBlock)
            {
                completeBlock.Initialize(enemy);
            }
        }
    }
}
