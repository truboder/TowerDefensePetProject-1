using Gameplay.Enemies;
using Gameplay.Tower.Projectiles;
using Gameplay.Tower.StaticData;
using UnityEngine;

namespace Gameplay.Tower.States
{
    public class AttackState : BaseTowerState
    {
        private readonly ProjectileFactory _projectileFactory;
        private readonly TowerSettings _settings;
        private float _lastAttackTime;

        public AttackState(TowerStateMachine stateMachine, Blackboard blackboard, GameObject owner, 
            ProjectileFactory projectileFactory, TowerSettings settings)
            : base(stateMachine, blackboard, owner)
        {
            _projectileFactory = projectileFactory;
            _settings = settings;
        }

        public override void Enter()
        {
            _lastAttackTime = Time.time;
        }

        public override void Update()
        {
            if (!Blackboard.TryGetData("Target", out Enemy target) || target == null)
            {
                StateMachine.SetState<TargetSelectionState>();
                return;
            }

            if (Time.time - _lastAttackTime < _settings.FireRate) return;

            Fire(target);
            _lastAttackTime = Time.time;
        }

        public override void Exit()
        {
        }

        private void Fire(Enemy target)
        {
            var projectile = _projectileFactory.Create();
            projectile.transform.position = Owner.transform.position;
            projectile.Initialize(target, _settings.Damage);
        }
    }
}