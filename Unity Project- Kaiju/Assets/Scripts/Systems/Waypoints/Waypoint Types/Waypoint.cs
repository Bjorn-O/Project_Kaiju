using UnityEngine;
using UnityEngine.Events;

public class Waypoint : MonoBehaviour
{
    public UnityEvent arrivalEvent;
    
    [Range(5.0f, 100.0f)]
    [SerializeField] private float curveStrength = 5f;
    [SerializeField] private float speed;

    private Vector3 _position;
    private BezierPath _path;
    
    public bool hasPath; 
    public float distance { get; private set; }

    public void SetPath(Waypoint endPoint)
    {
        _path = BezierPath.CreateInstance(transform, endPoint.GetTransform());
        if (_path != null) hasPath = true;
        distance = Vector3.Distance(transform.position, endPoint.GetTransform().position);
    }
    
    // Getter/Setter functions 
    public void SetTransform()
    {
        _position = transform.position;
    }

    public BezierPath GetPath()
    {
        return _path;
    }
    
    public Transform GetTransform()
    {
        var localTransform = transform;
        _position = localTransform.position;
        return localTransform;
    }

    public float GetCurveStrength()
    {
        return curveStrength;
    }

    // Utility function for Waypoint Manager workflow
    public void FlattenWaypoint()
    {
        var tempPos = transform.position;
        tempPos.y = 0;
        transform.position = tempPos;
    }
}

