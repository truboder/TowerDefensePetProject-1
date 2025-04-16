using UnityEngine;
using Zenject;

namespace Game.Enemy
{
    public class CompleteBlock : BehaviourBlock
    {
        private Enemy _enemy;

        public void Initialize(Enemy enemy)
        {
            _enemy = enemy;
        }

        public override void Process()
        {
            gameObject.SetActive(false);
        }
    }

}