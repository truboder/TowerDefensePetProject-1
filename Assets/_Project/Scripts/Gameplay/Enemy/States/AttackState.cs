using _Project.Scripts.Gameplay.Player;

namespace _Project.Scripts.Gameplay.Enemy.States
{
    public class AttackState : BaseEnemyState
    {
        private readonly HealthService _healthService;

        public AttackState(EnemyStateMachine stateMachine, Blackboard blackboard, Enemy enemy, HealthService healthService)
            : base(stateMachine, blackboard, enemy)
        {
            _healthService = healthService;
        }

        public override void Enter()
        {
            _healthService.TakeDamage(1);
            StateMachine.SetState<CompleteState>();
        }

        public override void Update()
        {

        }

        public override void Exit()
        {

        }
    }
}