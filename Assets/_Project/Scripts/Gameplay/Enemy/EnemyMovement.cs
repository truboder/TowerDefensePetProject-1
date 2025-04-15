using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _waypointBorder = 0.1f;

    private EnemyPath _enemyPath;

    private int _currentWaypointIndex = 0;
    private List<Vector3> _waypoints;
    private bool _hasPath = false;
    private bool _isMoving = true;
    private ILevelDataService _levelDataService;

    public event Action<EnemyMovement> PathCompleted;

    [Inject]
    public void Construct(ILevelDataService levelDataService)
    {
        _levelDataService = levelDataService;
    }

    private void Update()
    {
        if (!_hasPath || !_isMoving)
        {
            return;
        }

        MoveAlongPath();
    }

    private void OnDisable()
    {
        PathCompleted = null;
    }

    public void Initialize()
    {
        _enemyPath = _levelDataService.GetEnemyPath();
        InitializePath();
    }

    public void Cleanup()
    {
        _currentWaypointIndex = 0;
        _waypoints = null;
        _hasPath = false;
        _isMoving = true;
        PathCompleted = null;
    }

    public void InitializePath()
    {
        if (_enemyPath == null)
        {
            return;
        }

        _waypoints = _enemyPath.GetWaypointsPositions();

        if (_waypoints == null || _waypoints.Count == 0)
        {
            return;
        }

        _hasPath = true;
        transform.position = _waypoints[0];
        _currentWaypointIndex = 1;
    }

    public void StopMovement(bool stop = false)
    {
        _isMoving = stop;
    }

    public void ChangeSpeed(float newSpeed)
    {
        _moveSpeed = newSpeed;
    }

    private void MoveAlongPath()
    {
        float rotationSpeed = 10f;

        if (_currentWaypointIndex >= _waypoints.Count)
        {
            OnPathCompleted();
            return;
        }

        Vector3 targetPosition = _waypoints[_currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _moveSpeed * Time.deltaTime);

        if (transform.position != targetPosition)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }

        if (Vector3.Distance(transform.position, targetPosition) <= _waypointBorder)
        {
            _currentWaypointIndex++;
        }
    }

    private void OnPathCompleted()
    {
        PathCompleted?.Invoke(this);
    }
}