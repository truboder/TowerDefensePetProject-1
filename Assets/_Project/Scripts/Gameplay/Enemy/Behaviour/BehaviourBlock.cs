using UnityEngine;

namespace Game.Enemy
{
    public abstract class BehaviourBlock : MonoBehaviour
    {
        [SerializeField] protected BehaviourBlock _nextBlock;

        public void SetNext(BehaviourBlock next)
        {
            _nextBlock = next;
        }

        public abstract void Process();
    }
}