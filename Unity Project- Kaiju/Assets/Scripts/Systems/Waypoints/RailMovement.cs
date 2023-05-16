using Systems.Waypoints;
using UnityEngine;
using UnityEngine.InputSystem;


public class RailMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private Waypoint _currentWaypoint;
    private float _currentProgress;
    private float _waypointDistance;
    
    private void Start()
    {
        _currentWaypoint = WayPointManager.Instance.GetWaypoint();
        _currentProgress = 0;
    }

    private void UpdatePath()
    {
        _currentWaypoint = WayPointManager.Instance.GetWaypoint(_currentWaypoint);
        _waypointDistance = _currentWaypoint.distance;
        
    }

    private void Update()
    {
        if (!_currentWaypoint) return;
        transform.position = _currentWaypoint.GetPath().ReturnPosition(_currentProgress);
        _currentProgress += 0.01f * Time.deltaTime * (speed / _waypointDistance);
        transform.LookAt(_currentWaypoint.GetPath().ReturnPosition(_currentProgress));
        if (!(_currentProgress >= 1)) return;
        UpdatePath();
        _currentProgress = 0;
    }
}
