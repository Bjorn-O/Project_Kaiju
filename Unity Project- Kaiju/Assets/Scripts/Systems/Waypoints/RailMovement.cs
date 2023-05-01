using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;


public class RailMovement : MonoBehaviour
{
    public int i;
    public int totalWaypoints;
    public float speed;
    
    private Waypoint _lastWaypoint;
    private Waypoint _currentWaypoint;
    
    private void Start()
    {
        i = 0;
    }

    private void UpdatePath(Waypoint nextWaypoint)
    {
        _lastWaypoint = _currentWaypoint;
        _currentWaypoint = nextWaypoint;
        
    }

    private void Update()
    {
        
        // if(Vector3.Distance(transform.position, wp.waypoints[i].transform.position) == 0f)
        // {
        //     i++;
        //     
        //     print(i);
        // }
        // if(i >= totalWaypoints)
        // {
        //     i = 0;
        // }
        // print("Current Waypoint coörds = " + wp.waypoints[i].transform.position);
        // transform.LookAt(wp.waypoints[i].transform);
        // transform.position = Vector3.MoveTowards(transform.position, wp.waypoints[i].transform.position, speed * Time.deltaTime);
    }
}
