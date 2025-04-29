using System.Collections.Generic;
using Gameplay.Nemesis;
using Gameplay.Nemesis.Factory;
using Gameplay.Tower.StaticData;
using UnityEngine;
using Utils;

namespace Gameplay.Tower
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private TowerAttackSettings _settings;
        private readonly List<Enemy> _enemiesInRange = new List<Enemy>();
        private float _attackTimer;
        private ICoroutineRunService _coroutineRunner;

        public void Construct(ICoroutineRunService coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        private void Update()
        {
            UpdateEnemiesInRange();
            TryAttack();
        }

        private void UpdateEnemiesInRange()
        {
            _enemiesInRange.Clear();
            var enemies = FindObjectsOfType<Enemy>();

            foreach (var enemy in enemies)
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) <= _settings.AttackRadius)
                {
                    _enemiesInRange.Add(enemy);
                }
            }
        }

        private void TryAttack()
        {
            _attackTimer -= Time.deltaTime;
            if (_attackTimer > 0) return;

            Enemy target = FindClosestEnemy();
            if (target != null)
            {
                Attack(target);
                _attackTimer = _settings.AttackInterval;
            }
        }

        private Enemy FindClosestEnemy()
        {
            Enemy closestEnemy = null;
            float minDistance = float.MaxValue;

            foreach (var enemy in _enemiesInRange)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemy = enemy;
                }
            }

            return closestEnemy;
        }

        private void Attack(Enemy enemy)
        {
            enemy.TakeDamage(_settings.Damage);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _settings.AttackRadius);
        }
    }
}