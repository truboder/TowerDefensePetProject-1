namespace Infrastructure.StateMachine
{
    public abstract class BaseGameState
    {
        protected readonly IGameStateMachine StateMachine;

        protected BaseGameState(IGameStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}