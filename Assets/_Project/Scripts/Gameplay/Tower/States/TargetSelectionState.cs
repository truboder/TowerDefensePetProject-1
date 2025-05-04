using System.Collections.Generic;
using Gameplay.Enemies;
using Gameplay.Tower.StaticData;
using UnityEngine;

namespace Gameplay.Tower.States
{
    public class TargetSelectionState : BaseTowerState
    {
        private const string EnemiesInRangeKey = "EnemiesInRange";
        private const string TargetKey = "Target";
        
        private readonly TowerSettings _settings;

        public TargetSelectionState(TowerStateMachine stateMachine, Blackboard blackboard, GameObject onwer, TowerSettings settings)
            : base(stateMachine, blackboard, onwer)
        {
            _settings = settings;
        }

        public override void Enter()
        {
        }

        public override void Update()
        {
            if (Blackboard.TryGetData(EnemiesInRangeKey, out List<Enemy> enemies) && enemies.Count > 0)
            {
                Blackboard.TrySetData(TargetKey, enemies[0]);
                StateMachine.SetState<AttackState>();
            }
        }

        public override void Exit()
        {
        }
    }
}