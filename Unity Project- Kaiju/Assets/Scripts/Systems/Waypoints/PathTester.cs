using System;
using System.Net.Sockets;
using UnityEngine;

namespace Systems.Waypoints
{
    public class PathTester : MonoBehaviour
    {
        public Transform startTransform;
        public Transform endTransform;


        private Vector3 lastPos;
        private BezierPath _currentBezierPath;

        private void Start()
        {
            _currentBezierPath = BezierPath.CreateInstance(startTransform, endTransform);
        }

        private void Update()
        {
            _currentBezierPath.UpdatePath(startTransform, endTransform);
            
            for (float i = 0; i <= 1; i += 0.0625f)
            {
                Debug.DrawLine(lastPos, _currentBezierPath.ReturnPosition(i));
                lastPos = _currentBezierPath.ReturnPosition(i);
                if (i == 1)print(lastPos);
            }
        }
    }
}