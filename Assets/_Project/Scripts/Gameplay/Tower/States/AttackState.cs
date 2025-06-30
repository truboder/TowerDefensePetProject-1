using Common;
using Gameplay.Enemies;
using Gameplay.Tower.Projectiles.Factory;
using Gameplay.Tower.StaticData;
using UnityEngine;

namespace Gameplay.Tower.States
{
    public class AttackState : BaseTowerState
    {
        private const string TargetKey = "Target";
        private const float RotationSpeed = 10f;
        
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
        }

        public override void Update()
        {
            if (!Blackboard.TryGetData(TargetKey, out Enemy target) || target == null || !target.gameObject.activeSelf || !target.Health.IsAlive)
            {
                StateMachine.SetState<TargetSelectionState>();
                return;
            }
            
            var tower = Owner.GetComponent<Tower>();
            
            if (tower.Gun != null)
            {
                Vector3 direction = target.transform.position - tower.Gun.position;
                direction.y = 0;
                
                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    targetRotation *= Quaternion.Euler(0, 220, 0);
                    tower.Gun.rotation = Quaternion.Slerp(tower.Gun.rotation, targetRotation, RotationSpeed * Time.unscaledDeltaTime);
                }
            }

            Fire();
            StateMachine.SetState<CooldownState>();
        }

        public override void Exit()
        {
        }

        private void Fire()
        {
            if (!Blackboard.TryGetData(TargetKey, out Enemy target) || target == null || !target.gameObject.activeSelf || !target.Health.IsAlive)
            {
                StateMachine.SetState<TargetSelectionState>();
                return;
            }
            
            var tower = Owner.GetComponent<Tower>();
            var projectile = _projectileFactory.Create();
            
            projectile.transform.position = tower.ShotPoint != null ? tower.ShotPoint.position : Owner.transform.position;
            projectile.Initialize(target, _settings.Damage);
        }
    }
}