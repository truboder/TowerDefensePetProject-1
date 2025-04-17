using System;
using UnityEngine;
using Zenject;

namespace Game.Enemy
{
    public class CompleteBlock : BehaviourBlock
    {
        private Enemy _enemy;

        public event Action<Enemy> OnPathCompleted;

        public void Initialize(Enemy enemy)
        {
            _enemy = enemy;
        }

        public override void Process()
        {
            OnPathCompleted?.Invoke(_enemy);
        }
    }
}