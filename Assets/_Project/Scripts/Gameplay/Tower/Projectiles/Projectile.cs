using System;
using Gameplay.Enemies;
using UnityEngine;

namespace Gameplay.Tower.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        private const float HitDistance = 0.5f;
        private float _speed = 20f;
        private Enemy _target;
        private int _damage;

        public event Action<Projectile> OnTargetReached;

        public void Initialize(Enemy target, int damage)
        {
            _target = target;
            _damage = damage;
        }

        private void FixedUpdate()
        {
            if (_target == null || !_target.Health.IsAlive)
            {
                OnTargetReached?.Invoke(this);
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.fixedDeltaTime);

            if (Vector3.Distance(transform.position, _target.transform.position) <= HitDistance)
            {
                _target.Health.TakeDamage(_damage);
                OnTargetReached?.Invoke(this);
            }
        }
    }
}