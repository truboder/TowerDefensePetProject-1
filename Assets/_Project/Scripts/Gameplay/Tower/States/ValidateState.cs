using System.Collections.Generic;
using Common;
using Gameplay.Enemies;
using Gameplay.Tower.StaticData;
using UnityEngine;

namespace Gameplay.Tower.States
{
    public class ValidateState : BaseTowerState
    {
        private const string TargetKey = "Target";
        private const string EnemiesInRangeKey = "EnemiesInRange";

        private readonly TowerSettings _settings;

        public ValidateState(TowerStateMachine stateMachine, Blackboard blackboard, GameObject owner, TowerSettings settings)
            : base(stateMachine, blackboard, owner)
        {
            _settings = settings;
        }

        public override void Enter()
        {

        }

        public override void Update()
        {
            if (!Blackboard.TryGetData(TargetKey, out Enemy target) || target == null || !target.gameObject.activeSelf || !target.Health.IsAlive)
            {
                Blackboard.TryClearData(TargetKey);
                StateMachine.SetState<TargetSelectionState>();
                return;
            }
            
            if (Blackboard.TryGetData(EnemiesInRangeKey, out List<Enemy> enemies) && !enemies.Contains(target))
            {
                Blackboard.TryClearData(TargetKey);
                StateMachine.SetState<TargetSelectionState>();
                return;
            }
            
            StateMachine.SetState<AttackState>();
        }

        public override void Exit()
        {

        }
    }
}