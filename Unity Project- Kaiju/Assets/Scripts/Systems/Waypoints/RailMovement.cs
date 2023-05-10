using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Systems.Waypoints;
using UnityEngine;


public class RailMovement : MonoBehaviour
{
    public float speed;
    
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

    // private void Update()
    // {
    //     
    //     //if(Vector3.Distance(transform.position, wp.waypoints[i].transform.position) == 0f)
    //     //{
    //     //    i++;
    //         
    //     //    print(i);
    //     //}
    //     //if(i >= totalWaypoints)
    //     //{
    //     //    i = 0;
    //     //}
    //     //print("Current Waypoint coörds = " + wp.waypoints[i].transform.position);
    //     //transform.LookAt(wp.waypoints[i].transform);
    //     //transform.position = Vector3.MoveTowards(transform.position, wp.waypoints[i].transform.position, speed * Time.deltaTime);
    // }
}
