using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Toolbox.Attributes;
using Toolbox.MethodExtensions;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace Systems.Waypoints
{
    public class WayPointManager : MonoBehaviour
    {
        public static WayPointManager Instance; 
        public List<Waypoint> waypointsList = new();

        [SerializeField] private bool isLoop;
        [SerializeField] private int gizmoLines;
        [SerializeField] private GameObject waypointGameObject;
        [SerializeField] private GameObject brakePointGameObject;
        [SerializeField] private GameObject splitPointGameObject;
        [SerializeField] private GameObject waitPointGameObject;

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
        
        //Overload that returns the next possible waypoint. 

        public Waypoint GetWaypoint(Waypoint currentWaypoint)
        {
            return waypointsList.GetNextPossibleItem(currentWaypoint);
        }
        
        //Overload that returns the first waypoint
        public Waypoint GetWaypoint()
        {
            return waypointsList[0];
        }

        public bool RequestToContinue(Waypoint point)
        {
            return (waypointsList.IsLast(point) && isLoop || !waypointsList.IsLast(point));
        }
    
        //In-Editor tool for showing the correct curves of they waypoints
        private void OnDrawGizmos()
        {
            foreach (var waypoint in waypointsList)
            {
                if(!isLoop && waypointsList.IsLast(waypoint) || waypoint.GetPath() == null) return;
                var path = waypoint.GetPath();
                path.UpdatePath(waypoint.GetTransform(), waypointsList.GetNextPossibleItem(waypoint).GetTransform(), waypoint.GetCurveStrength());
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

        [Button]
        private void GetWaypointTransforms()
        {
            foreach (var waypoint in waypointsList)
            {
                waypoint.SetTransform();
                print(waypoint.name + "Position is set.");
            }
        }

        [Button]
        private void LevelWaypoints()
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

        [Button]
        private void SpawnWaypoint(WaypointTypes waypointType)
        {
            var targetPrefab = waypointType switch
            {
                WaypointTypes.Waypoint => waypointGameObject,
                WaypointTypes.Brakepoint => brakePointGameObject,
                WaypointTypes.Splitpoint => splitPointGameObject,
                WaypointTypes.Waitpoint => waitPointGameObject,
                _ => throw new ArgumentOutOfRangeException(nameof(waypointType), waypointType, null)
            };

            var waypoint = waypointsList.Count > 0 ?
                Instantiate(targetPrefab, waypointsList[^1].GetTransform().position, waypointsList[^1].GetTransform().rotation) :
                Instantiate(targetPrefab, Vector3.zero, Quaternion.identity );
            waypoint.transform.parent = this.transform;
            waypointsList.Add(waypoint.GetComponent<Waypoint>());
            waypoint.name = $"Waypoint{waypointsList.Count - 1}";
            
            SetWaypointPaths();
        }
    }
}