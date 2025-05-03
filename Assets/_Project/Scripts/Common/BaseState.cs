using Gameplay.Enemies;

namespace Common
{
    public abstract class BaseState
    {
        protected readonly StateMachine StateMachine;
        protected readonly Blackboard Blackboard;

        protected BaseState(StateMachine stateMachine, Blackboard blackboard)
        {
            StateMachine = stateMachine;
            Blackboard = blackboard;
        }

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}