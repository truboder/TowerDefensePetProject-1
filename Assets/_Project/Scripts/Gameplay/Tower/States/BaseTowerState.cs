using Gameplay.Enemies;

namespace Gameplay.Tower.States
{
    public abstract class BaseTowerState
    {
        protected BaseTowerState(TowerStateMachine stateMachine, Blackboard blackboard, Tower tower)
        {
            StateMachine = stateMachine;
            Blackboard = blackboard;
            Tower = tower;
        }
        
        protected TowerStateMachine StateMachine { get; }
        protected Blackboard Blackboard { get; }
        protected Tower Tower { get; }

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}