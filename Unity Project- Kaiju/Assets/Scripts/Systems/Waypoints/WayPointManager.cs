using System;
using System.Collections.Generic;
using System.Linq;
using Toolbox.Attributes;
using Toolbox.MethodExtensions;
using UnityEngine;

namespace Systems.Waypoints
{
    public class WayPointManager : MonoBehaviour
    {
        public static WayPointManager Instance; 
        public List<Waypoint> waypointsList = new();

        [SerializeField] private int gizmoLines;

        private void Awake()
        {
            if (Instance != null && Instance !=this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
            
            SetWaypointPaths();
        }

        public Waypoint GetWaypoint(Waypoint currentWaypoint)
        {
            return waypointsList.GetNextPossibleItem(currentWaypoint);
        }

        public Waypoint GetWaypoint()
        {
            return waypointsList[0];
        }
    
        private void OnDrawGizmosSelected()
        {
            foreach (var waypoint in waypointsList)
            {
                var path = waypoint.GetPath();
                path.UpdatePath(waypoint.GetTransform(), waypointsList.GetNextPossibleItem(waypoint).GetTransform());
                var lastPos = waypoint.GetTransform().position;
                for (var i = 0; i <= gizmoLines; i++)
                {
                    var x = (1f / gizmoLines) * i;
                    Gizmos.DrawLine(lastPos, waypoint.GetPath().ReturnPosition(x));
                    Gizmos.color = Color.green;
                    lastPos = waypoint.GetPath().ReturnPosition(x);
                }
            }
        }
        
        // Helper functions to be called through their inspector buttons
        [Button]
        public void InitializeWaypoints()
        {
            print(" triggered by using a button in the inspector ");
            var components = GameObject.FindObjectsOfType<Waypoint>();
            print(components.Length);
            waypointsList = components.ToList();
            waypointsList.Sort();
        }

        [Button(Mode = ButtonMode.AlwaysEnabled)]
        private void GetWaypointTransforms()
        {
            foreach (var waypoint in waypointsList)
            {
                waypoint.SetTransform();
                print(waypoint.name + "Position is set.");
            }
        }

        [Button(Mode = ButtonMode.AlwaysEnabled)]
        private void LevelWaypointsToZ0()
        {
            foreach (var waypoint in waypointsList)
            {
                waypoint.FlattenWaypoint();
            }
        }

        [Button]
        private void SetWaypointPaths()
        {
            foreach (var waypoint in waypointsList)
            {
                waypoint.SetPath(waypointsList.GetNextPossibleItem(waypoint));
            }
        }
    }
}