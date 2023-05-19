using System.Collections;
using Systems.Waypoints;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;


public class RailMovement : MonoBehaviour
{
    [SerializeField] private float originalSpeed;
    [SerializeField] private float easeInTolerance = 0.1f;
    [Range(0.1f, 1.0f)]
    [SerializeField] private float minimumSpeedPercentage;

    private Waypoint _currentWaypoint;
    private bool _updatingWaypoint;
    private float _speed;
    private float _currentProgress;
    private float _waypointDistance;
    
    private void Start()
    {
        _currentWaypoint = WayPointManager.Instance.GetWaypoint();
        _currentProgress = 0;
        _speed = originalSpeed;
    }

    private async void UpdateWaypoint()
    {
        _updatingWaypoint = true;
        var wayPoint = WayPointManager.Instance.GetWaypoint(_currentWaypoint);

        _currentProgress = 0;

        switch (wayPoint)
        {
            case Brakepoint:
                StartCoroutine(EaseOut());
                break;
            
            case Splitpoint splitPoint:
                wayPoint = splitPoint.ReturnIntendedPoint();
                break;
            
            case WaitPoint waitPoint:
                await waitPoint.WaitForGreenLight();
                StartCoroutine(EaseIn());
                break;
            
            default:
                if (_speed < originalSpeed) StartCoroutine(EaseIn()); 
                break;
        }
        SetWaypoint(wayPoint);
        _updatingWaypoint = false;
    }

    private void Update()
    {
        if (_currentWaypoint && _currentProgress <= 1f && !_updatingWaypoint)
        {
            transform.position = _currentWaypoint.GetPath().ReturnPosition(_currentProgress);
            _currentProgress += 0.01f * Time.deltaTime * _speed;
            transform.LookAt(_currentWaypoint.GetPath().ReturnPosition(_currentProgress));
            return;
        }
        if (!_updatingWaypoint)
        {
            UpdateWaypoint();
        }
    }

    private void SetWaypoint(Waypoint point)
    {
        _currentWaypoint = point;
        _waypointDistance = point.distance;
    }

    private IEnumerator EaseOut()
    {
        while (_currentProgress <= 1)
        {
            _speed = Mathf.Max(originalSpeed * minimumSpeedPercentage, originalSpeed * (1 - _currentProgress));
            yield return null;
        }
    }
    
    private IEnumerator EaseIn()
    {
        while (_currentProgress <= 1)
        {
            _speed = Mathf.Max(originalSpeed * minimumSpeedPercentage, originalSpeed * (_currentProgress + easeInTolerance));
            yield return null;
        }
    }
}
