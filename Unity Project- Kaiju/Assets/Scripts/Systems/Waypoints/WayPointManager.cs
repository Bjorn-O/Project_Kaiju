using System;
using System.Collections.Generic;
using System.Linq;
using Toolbox.Attributes;
using Toolbox.MethodExtensions;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

namespace Systems.Waypoints
{
    public class WayPointManager : MonoBehaviour
    {
        public static WayPointManager Instance; 
        public List<Waypoint> waypointsList = new();

        [SerializeField] private int gizmoLines;
        [SerializeField] private GameObject waypointGameObject;

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
    
        //In-Editor tool for showing the correct curves of they waypoints
        private void OnDrawGizmos()
        {
            foreach (var waypoint in waypointsList)
            {
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

        [Button]
        private void SpawnWaypoint()
        {
            GameObject waypoint; 
            if (waypointsList.Count > 0)
            {
                waypoint = Instantiate(waypointGameObject, waypointsList[^1].GetTransform().position, waypointsList[^1].GetTransform().rotation);
                waypoint.transform.parent = this.transform;
                waypointsList.Add(waypoint.GetComponent<Waypoint>());
            }
            else
            {
                waypoint = Instantiate(waypointGameObject, Vector3.zero, quaternion.identity );
                waypoint.transform.parent = this.transform;
                waypointsList.Add(waypoint.GetComponent<Waypoint>());
            }
            waypoint.name = $"Waypoint{waypointsList.Count - 1}";
            
            SetWaypointPaths();
        }

        [Button]
        private void InsertNextWaypoint()
        {
            Debug.Log("Called");
            foreach (var waypoint in waypointsList)
            {
                if (Selection.Contains(waypoint.gameObject))
                {
                    Debug.Log("Heard");
                    var waypointTransform = waypoint.GetTransform();
                    var newPoint = Instantiate(waypointGameObject, waypointTransform.position,
                        waypointTransform.rotation);
                    newPoint.transform.parent = this.transform;
                    waypointsList.Insert(waypointsList.IndexOf(waypoint) +1, newPoint.GetComponent<Waypoint>());
                    SetWaypointPaths();
                }
            }
        }
        [Button]
        private void InsertPreviousWaypoint()
        {
            var possibleSelectedPoints = waypointsList.Where(waypoint => Selection.Contains(waypoint.gameObject)).ToList();
            if (possibleSelectedPoints.IsEmpty()) return;
            var waypoint = possibleSelectedPoints.First();
            
            var waypointTransform = waypoint.GetTransform();
            var useRotation = waypointTransform.rotation;
            var usePosition = waypointTransform.position;
           
            var newPoint = Instantiate(waypointGameObject, usePosition, useRotation, transform);
            waypointsList.Insert(waypointsList.IndexOf(waypoint), newPoint.GetComponent<Waypoint>());
            SetWaypointPaths();
            
            
            //has nesting but should add one for each
            // foreach (var waypoint in possibleSelectedPoints)
            // {
            //     Debug.Log("Heard");
            //     var waypointTransform = waypoint.GetTransform();
            //     var newPoint = Instantiate(waypointGameObject, waypointTransform.position,
            //         waypointTransform.rotation);
            //     newPoint.transform.parent = this.transform;
            //     waypointsList.Insert(waypointsList.IndexOf(waypoint), newPoint.GetComponent<Waypoint>());
            //     SetWaypointPaths();
            // }
        }
    }
}