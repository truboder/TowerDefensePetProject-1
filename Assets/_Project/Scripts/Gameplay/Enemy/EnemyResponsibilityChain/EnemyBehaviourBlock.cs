using UnityEngine;

public abstract class EnemyBehaviourBlock : MonoBehaviour
{
    [SerializeField] protected EnemyBehaviourBlock _nextBlock;

    public void SetNext(EnemyBehaviourBlock next)
    {
        _nextBlock = next;
    }

    public abstract void Run();
}
