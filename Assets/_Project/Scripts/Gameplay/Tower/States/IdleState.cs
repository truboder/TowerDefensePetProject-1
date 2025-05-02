using System.Collections.Generic;
using Gameplay.Enemies;
using Gameplay.Tower.StaticData;

namespace Gameplay.Tower.States
{
    public class IdleState : BaseTowerState
    {
        private readonly TowerSettings _settings;

        public IdleState(TowerStateMachine stateMachine, Blackboard blackboard, Gameplay.Tower.Tower tower, TowerSettings settings)
            : base(stateMachine, blackboard, tower)
        {
            _settings = settings;
        }

        public override void Enter()
        {
        }

        public override void Update()
        {
            if (Blackboard.TryGetData("EnemiesInRange", out List<Enemy> enemies) && enemies.Count > 0)
            {
                Blackboard.TrySetData("Target", enemies[0]);
                StateMachine.SetState<AttackState>();
            }
        }

        public override void Exit()
        {
        }
    }
}