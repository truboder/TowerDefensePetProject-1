namespace _Project.Scripts.Gameplay.Enemy.States
{
    public abstract class BaseEnemyState
    {
        protected BaseEnemyState(EnemyStateMachine stateMachine, Blackboard blackboard, Enemy enemy)
        {
            StateMachine = stateMachine;
            Blackboard = blackboard;
            Enemy = enemy;
        }
        
        protected EnemyStateMachine StateMachine { get; }
        protected Blackboard Blackboard { get; }
        protected Enemy Enemy { get; }

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}