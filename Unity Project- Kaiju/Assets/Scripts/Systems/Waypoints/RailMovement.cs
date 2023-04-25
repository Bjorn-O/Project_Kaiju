using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;


public class RailMovement : MonoBehaviour
{
    public Waypoints wp;
    //private Transform currentWaypoint;
    public int i;

    private void Start()
    {
        i = 0;
    }

    private void Update()
    {
        
        if(Vector3.Distance(transform.position, wp.waypoints[i].transform.position) == 0f)
        {
            i++;
            print(i);
        }
        if(i >= 4)
        {
            i = 0;
        }
        print("Current Waypoint coörds = " + wp.waypoints[i].transform.position);
        transform.position = Vector3.MoveTowards(transform.position, wp.waypoints[i].transform.position, 1 * Time.deltaTime);
    }
}
