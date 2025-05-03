using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Enemies.States
{
    public class AttackState : BaseEnemyState
    {
        private readonly HealthService _healthService;

        public AttackState(EnemyStateMachine stateMachine, Blackboard blackboard, GameObject owner, HealthService healthService)
            : base(stateMachine, blackboard, owner)
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