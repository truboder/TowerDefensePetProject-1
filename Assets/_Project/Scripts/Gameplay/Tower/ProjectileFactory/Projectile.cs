using Gameplay.Enemies;
using UnityEngine;
using System;

namespace Gameplay.Tower
{
    public class Projectile : MonoBehaviour
    {
        private Enemy _target;
        private float _speed = 20f;
        private int _damage;
        private const float _hitDistance = 0.5f;

        public event Action OnTargetReached;

        public void Initialize(Enemy target, int damage)
        {
            _target = target;
            _damage = damage;
        }

        private void Update()
        {
            if (_target == null || !_target.HealthService.IsAlive)
            {
                OnTargetReached?.Invoke();
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _target.transform.position) <= _hitDistance)
            {
                _target.HealthService.TakeDamage(_damage);
                OnTargetReached?.Invoke();
            }
        }
    }
}