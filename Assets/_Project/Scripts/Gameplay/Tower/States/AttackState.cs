using System.Collections.Generic;
using Gameplay.Enemies;
using Gameplay.Tower.StaticData;
using UnityEngine;

namespace Gameplay.Tower.States
{
    public class AttackState : BaseTowerState
    {
        private readonly ProjectileFactory.ProjectileFactory _projectileFactory;
        private readonly TowerSettings _settings;
        private float _lastAttackTime;

        public AttackState(TowerStateMachine stateMachine, Blackboard blackboard, Tower tower, 
            ProjectileFactory.ProjectileFactory projectileFactory, TowerSettings settings)
            : base(stateMachine, blackboard, tower)
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
            if (!Blackboard.TryGetData("EnemiesInRange", out List<Enemy> enemies) || enemies.Count == 0)
            {
                StateMachine.SetState<IdleState>();
                return;
            }

            if (!Blackboard.TryGetData("Target", out Enemy target) || target == null)
            {
                Blackboard.TrySetData("Target", enemies[0]);
                target = enemies[0];
            }

            if (Time.time - _lastAttackTime >= _settings.FireRate)
            {
                Fire(target);
                _lastAttackTime = Time.time;
            }
        }

        public override void Exit()
        {
        }

        private void Fire(Enemy target)
        {
            var projectile = _projectileFactory.Create();
            projectile.transform.position = Tower.GetPosition();
            projectile.Initialize(target, _settings.Damage);
        }
    }
}