using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] _Waypoints;
    [SerializeField] private float _Speed = 1f;
    private int _currentWaypointIndex = 0;

    private void Update()
    {
        if (Vector3.Distance(transform.position, _Waypoints[_currentWaypointIndex].transform.position) < 0.1f)
        {
            // If the object is close enough to the current waypoint, move to the next waypoint
            _currentWaypointIndex++;

            // If we've reached the last waypoint, loop back to the first one
            if (_currentWaypointIndex >= _Waypoints.Length)
            {
                _currentWaypointIndex = 0;
            }
        }
        // Move the platform to a waypoint
        transform.position = Vector3.MoveTowards(transform.position, _Waypoints[_currentWaypointIndex].transform.position, _Speed * Time.deltaTime);
    }
}
