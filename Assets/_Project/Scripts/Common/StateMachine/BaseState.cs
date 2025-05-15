using Gameplay.Enemies;

namespace Common.StateMachine
{
    public abstract class BaseState
    {
        protected readonly Common.StateMachine.StateMachine StateMachine;
        protected readonly Blackboard Blackboard;

        protected BaseState(Common.StateMachine.StateMachine stateMachine, Blackboard blackboard)
        {
            StateMachine = stateMachine;
            Blackboard = blackboard;
        }

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}