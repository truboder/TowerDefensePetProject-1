using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyMoveBlock : EnemyBehaviourBlock
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _waypointBorder = 0.1f;

    private List<Vector3> _waypoints;
    private int _currentWaypointIndex;
    private bool _hasPath;
    private int minWaypointsCount = 1;

    public void InitializePath(List<Vector3> waypoints)
    {
        _waypoints = waypoints;
        _currentWaypointIndex = 1;
        _hasPath = waypoints != null && waypoints.Count > minWaypointsCount;
        transform.position = waypoints[0];
    }

    private void Update()
    {
        if (_hasPath)
        {
            Move();
        }
    }

    private void Move()
    {
        float rotationSpeed = 10f;

        if (_currentWaypointIndex >= _waypoints.Count)
        {
            Run();
            enabled = false;
            return;
        }

        Vector3 target = _waypoints[_currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, target, _moveSpeed * Time.deltaTime);

        if (transform.position != target)
        {
            Vector3 moveDirection = (target - transform.position).normalized;
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }

        if (Vector3.Distance(transform.position, target) <= _waypointBorder)
        {
            _currentWaypointIndex++;
        }
    }

    public override void Run()
    {
        _nextBlock?.Run();
    }
}
