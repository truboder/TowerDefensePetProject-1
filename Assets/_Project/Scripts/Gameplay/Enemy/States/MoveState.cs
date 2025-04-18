using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Enemy.States
{
    public class MoveState : BaseEnemyState
    {
        private const int MinWaypointsCount = 1;
        private const float WaypointBorder = 0.1f;
        private const float MoveSpeed = 10f;
        private const float RotationSpeed = 10f;

        private List<Vector3> _waypoints;
        private int _currentWaypointIndex;
        private bool _hasPath;

        public MoveState(EnemyStateMachine stateMachine, Blackboard blackboard, Enemy enemy)
            : base(stateMachine, blackboard, enemy)
        {
        }

        public override void Enter()
        {
            if (!Blackboard.TryGetData("Waypoints", out _waypoints))
            {
                _hasPath = false;
                return;
            }

            _currentWaypointIndex = 1;
            _hasPath = _waypoints != null && _waypoints.Count > MinWaypointsCount;
            Enemy.transform.position = _waypoints[0];
        }

        public override void Update()
        {
            if (!_hasPath)
            {
                StateMachine.SetState<AttackState>();
                return;
            }

            if (_currentWaypointIndex >= _waypoints.Count)
            {
                StateMachine.SetState<AttackState>();
                return;
            }

            Vector3 target = _waypoints[_currentWaypointIndex];
            Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, target, MoveSpeed * Time.deltaTime);

            if (Enemy.transform.position != target)
            {
                Vector3 moveDirection = (target - Enemy.transform.position).normalized;
                if (moveDirection != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                    Enemy.transform.rotation = Quaternion.Slerp(Enemy.transform.rotation, targetRotation, Time.deltaTime * RotationSpeed);
                }
            }

            if (Vector3.Distance(Enemy.transform.position, target) <= WaypointBorder)
            {
                _currentWaypointIndex++;
            }
        }

        public override void Exit()
        {

        }
    }
}