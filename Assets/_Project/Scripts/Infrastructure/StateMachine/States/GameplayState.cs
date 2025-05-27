using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.StateMachine.States
{
    public class GameplayState : BaseGameState
    {
        public GameplayState(IGameStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            if (SceneManager.GetActiveScene().name != "GameplayScene")
            {
                SceneManager.LoadScene("GameplayScene");
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