using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Systems.Waypoints;
using UnityEngine;


public class RailMovement : MonoBehaviour
{
    private Waypoint _currentWaypoint;
    
    private void Start()
    {
        _currentWaypoint = WayPointManager.Instance.GetWaypoint();
    }

    private void UpdatePath(Waypoint nextWaypoint)
    {
        _currentWaypoint = nextWaypoint;
        
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
