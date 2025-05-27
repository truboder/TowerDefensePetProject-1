using UnityEngine;

namespace Infrastructure.StateMachine.States
{
    public class BootstrapState : BaseGameState
    {
        public BootstrapState(IGameStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            StateMachine.Enter<GameplayState>();
        }

        public override void Update() { }

        public override void Exit()
        {

        }
    }
}