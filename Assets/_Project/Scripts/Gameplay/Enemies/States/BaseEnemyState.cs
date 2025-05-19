using Common;
using Common.StateMachine;
using UnityEngine;

namespace Gameplay.Enemies.States
{
    public abstract class BaseEnemyState : BaseState
    {
        protected readonly GameObject Owner;

        protected BaseEnemyState(StateMachine stateMachine, Blackboard blackboard, GameObject owner)
            : base(stateMachine, blackboard)
        {
            Owner = owner;
        }
    }
}