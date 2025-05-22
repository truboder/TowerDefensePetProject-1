using Common;
using Gameplay.Tower.StaticData;
using UnityEngine;

namespace Gameplay.Tower.States
{
    public class CooldownState : BaseTowerState
    {
        private readonly TowerSettings _settings;
        private float _startTime;

        public CooldownState(TowerStateMachine stateMachine, Blackboard blackboard, GameObject owner, TowerSettings settings)
            : base(stateMachine, blackboard, owner)
        {
            _settings = settings;
        }

        public override void Enter()
        {
            _startTime = Time.time;
        }

        public override void Update()
        {
            if (Time.time - _startTime >= _settings.FireRate)
            {
                StateMachine.SetState<ValidateState>();
            }
        }

        public override void Exit()
        {
            
        }
    }
}