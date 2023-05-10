using UnityEditor;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Vector3 position;
    [SerializeField] private float speed;
    public bool hasPath;

    public float distance { get; private set; }

    private BezierPath _path;
    private Waypoint _destination;

    public void SetPath(Waypoint endPoint)
    {
        _path = BezierPath.CreateInstance(transform, endPoint.GetTransform());
        if (_path != null) hasPath = true;
        _destination = endPoint;
        distance = Vector3.Distance(transform.position, endPoint.GetTransform().position);
    }

    public BezierPath GetPath()
    {
        return _path;
    }

    public void SetTransform()
    {
        position = transform.position;
    }

    public Transform GetTransform()
    {
        var localTransform = transform;
        position = localTransform.position;
        return localTransform;
    }

    public void FlattenWaypoint()
    {
        var tempPos = transform.position;
        tempPos.y = 0;
        transform.position = tempPos;
        position = tempPos;
    }
}

