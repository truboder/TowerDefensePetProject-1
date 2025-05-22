using Common;
using Common.StateMachine;
using UnityEngine;

namespace Gameplay.Tower.States
{
    public abstract class BaseTowerState : BaseState
    {
        protected readonly GameObject Owner;

        protected BaseTowerState(StateMachine stateMachine, Blackboard blackboard, GameObject owner)
            : base(stateMachine, blackboard)
        {
            Owner = owner;
        }
    }
}