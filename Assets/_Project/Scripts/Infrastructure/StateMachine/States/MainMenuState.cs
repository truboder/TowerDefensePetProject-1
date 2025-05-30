using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.StateMachine.States
{
    public class MainMenuState : BaseGameState
    {
        public MainMenuState(IGameStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            if (SceneManager.GetActiveScene().name != "MainMenuScene")
            {
                SceneManager.LoadScene("MainMenuScene");
            }
        }

        public override void Update()
        {
        }

        public override void Exit()
        {
        }
    }
}