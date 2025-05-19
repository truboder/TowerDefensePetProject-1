using Gameplay.Enemies;
using Gameplay.Tower.Projectiles.Factory;
using Gameplay.Tower.StaticData;
using UnityEngine;

namespace Gameplay.Tower.States
{
    public class AttackState : BaseTowerState
    {
        private const string TargetKey = "Target";
        
        private readonly IProjectileFactory _projectileFactory;
        private readonly TowerSettings _settings;

        public AttackState(TowerStateMachine stateMachine, Blackboard blackboard, GameObject owner, 
            IProjectileFactory projectileFactory, TowerSettings settings)
            : base(stateMachine, blackboard, owner)
        {
            _projectileFactory = projectileFactory;
            _settings = settings;
        }

        public override void Enter()
        {
            Fire();
        }

        public override void Update()
        {
        }

        public override void Exit()
        {
        }

        private void Fire()
        {
            if (!Blackboard.TryGetData(TargetKey, out Enemy target) || target == null)
            {
                StateMachine.SetState<TargetSelectionState>();
                return;
            }
            
            var projectile = _projectileFactory.Create();
            
            projectile.transform.position = Owner.transform.position;
            projectile.Initialize(target, _settings.Damage);

            StateMachine.SetState<CooldownState>();
        }
    }
}