using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints = new List<Transform>();
    [SerializeField] private bool drawGizmos = true;
    [SerializeField] private Color pathColor = Color.red;

    public List<Transform> Waypoints => waypoints;

    public List<Vector3> GetWaypointsPositions()
    {
        List<Vector3> positions = new List<Vector3>();

        foreach (Transform waypoint in waypoints)
        {
            positions.Add(waypoint.position);
        }

        return positions;
    }

    public void AddWaypoint(Transform newWaypoint)
    {
        waypoints.Add(newWaypoint);
    }

    public void RemoveWaypoint(Transform waypointToRemove)
    {
        waypoints.Remove(waypointToRemove);
    }

    private void OnDrawGizmos()
    {
        if (!drawGizmos || waypoints == null || waypoints.Count < 2)
        {
            return;
        }

        Gizmos.color = pathColor;

        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            if (waypoints[i] != null && waypoints[i + 1] != null)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
        }

        foreach (Transform waypoint in waypoints)
        {
            if (waypoint != null)
            {
                Gizmos.DrawSphere(waypoint.position, 0.2f);
            }
        }
    }
}
