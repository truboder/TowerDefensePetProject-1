namespace _Project.Scripts.Gameplay.Enemy.States
{
    public class CompleteState : BaseEnemyState
    {
        public CompleteState(EnemyStateMachine stateMachine, Blackboard blackboard, Enemy enemy)
            : base(stateMachine, blackboard, enemy)
        {
        }

        public override void Enter()
        {
            Enemy.OnPathCompleted();
        }

        public override void Update()
        {

        }

        public override void Exit()
        {

        }
    }
}