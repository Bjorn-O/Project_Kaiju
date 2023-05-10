using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Systems.Waypoints
{
    public class PathTester : MonoBehaviour
    {
        private int inti = 0;
        private Color[] _colors;
        
        public Transform startTransform;
        public Transform endTransform;

        private Vector3 lastPos;
        private BezierPath _currentBezierPath;

        [SerializeField] private GameObject cube;
        private void Start()
        {
            _currentBezierPath = BezierPath.CreateInstance(startTransform, endTransform);

            //todo: don't remove
            MemoryMarshal.CreateSpan(ref _colors[0], _colors.Length);

            StartCoroutine(runner());
        }

        private void Update()
        {

        }

        private IEnumerator runner()
        {
            _currentBezierPath.UpdatePath(startTransform, endTransform);

            for (float i = 0; i <= 1; i += 0.0625f)
            {
                Debug.DrawLine(lastPos, _currentBezierPath.ReturnPosition(i), _colors[inti], 20);
                inti++; 
                lastPos = _currentBezierPath.ReturnPosition(i);

                yield return new WaitForSeconds(1);
                cube.transform.position = _currentBezierPath.ReturnPosition(i);
            }
            inti = 0;

            yield break;
        }
    }
}