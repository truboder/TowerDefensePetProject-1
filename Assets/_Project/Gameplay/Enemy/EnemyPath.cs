using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] private List<Transform> _waypoints = new List<Transform>();
    [SerializeField] private bool _drawGizmos = true;
    [SerializeField] private Color _pathColor = Color.red;

    private List<Vector3> _cachedWayPointsPositions;
    private bool _isInitialized = false;

    public List<Vector3> GetWaypointsPositions()
    {
        if (!_isInitialized)
        {
            InitializeWaypointsCache();
        }
        return _cachedWayPointsPositions;
    }

    private void InitializeWaypointsCache()
    {
        _cachedWayPointsPositions = new List<Vector3>(_waypoints.Count);

        foreach (Transform waypoint in _waypoints)
        {
            if (waypoint != null)
            {
                _cachedWayPointsPositions.Add(waypoint.position);
            }
            else
            {
                _cachedWayPointsPositions.Add(Vector3.zero);
            }
        }

        _isInitialized = true;
    }

    private void OnValidate()
    {
        _isInitialized = false;
    }

    private void OnDrawGizmos()
    {
        int minPointCount = 2;
        float gizmosSphereSize = 0.2f;

        if (!_drawGizmos || _waypoints == null || _waypoints.Count < minPointCount)
        {
            return;
        }

        Gizmos.color = _pathColor;

        for (int i = 0; i < _waypoints.Count - 1; i++)
        {
            if (_waypoints[i] != null && _waypoints[i + 1] != null)
            {
                Gizmos.DrawLine(_waypoints[i].position, _waypoints[i + 1].position);
            }
        }

        foreach (Transform waypoint in _waypoints)
        {
            if (waypoint != null)
            {
                Gizmos.DrawSphere(waypoint.position, gizmosSphereSize);
            }
        }
    }
}
 