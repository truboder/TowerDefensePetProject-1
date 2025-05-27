using UnityEngine;

namespace Infrastructure.StateMachine.States
{
    public class MainMenuState : BaseGameState
    {
        public MainMenuState(IGameStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {

        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StateMachine.Enter<GameplayState>();
            }
        }

        public override void Exit()
        {

        }
    }
}