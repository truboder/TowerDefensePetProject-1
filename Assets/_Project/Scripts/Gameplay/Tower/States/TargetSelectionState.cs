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

        public TargetSelectionState(TowerStateMachine stateMachine, Blackboard blackboard, GameObject owner, TowerSettings settings)
            : base(stateMachine, blackboard, owner)
        {
            _settings = settings;
        }

        public override void Enter()
        {

        }

        public override void Update()
        {
            if (!Blackboard.TryGetData(EnemiesInRangeKey, out List<Enemy> enemies))
            {
                return;
            }
            
            enemies.RemoveAll(enemy => enemy == null || !enemy.gameObject.activeSelf || !enemy.Health.IsAlive);

            if (enemies.Count > 0)
            {
                var target = enemies[0];
                
                Blackboard.TrySetData(TargetKey, target);
                StateMachine.SetState<ValidateState>();
            }
        }

        public override void Exit()
        {

        }
    }
}