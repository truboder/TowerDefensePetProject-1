using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Enemies.States
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

        public MoveState(EnemyStateMachine stateMachine, Blackboard blackboard, GameObject owner)
            : base(stateMachine, blackboard, owner)
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
            Owner.transform.position = _waypoints[0];
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
            Owner.transform.position = Vector3.MoveTowards(Owner.transform.position, target, MoveSpeed * Time.deltaTime);

            if (Owner.transform.position != target)
            {
                Vector3 moveDirection = (target - Owner.transform.position).normalized;
                if (moveDirection != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                    Owner.transform.rotation = Quaternion.Slerp(Owner.transform.rotation, targetRotation, Time.deltaTime * RotationSpeed);
                }
            }

            if (Vector3.Distance(Owner.transform.position, target) <= WaypointBorder)
            {
                _currentWaypointIndex++;
            }
        }

        public override void Exit()
        {

        }
    }
}