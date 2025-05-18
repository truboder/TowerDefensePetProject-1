using Gameplay.PlayerCastle;
using Gameplay.Levels;
using UnityEngine;

namespace Gameplay.Enemies.States
{
    public class AttackState : BaseEnemyState
    {
        private readonly ILevelDataService _levelDataService;

        public AttackState(EnemyStateMachine stateMachine, Blackboard blackboard, GameObject owner, ILevelDataService levelDataService)
            : base(stateMachine, blackboard, owner)
        {
            _levelDataService = levelDataService;
        }

        public override void Enter()
        {
            Castle castle = _levelDataService.GetCastle();
            if (castle != null)
            {
                castle.Health.TakeDamage(1);
            }
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