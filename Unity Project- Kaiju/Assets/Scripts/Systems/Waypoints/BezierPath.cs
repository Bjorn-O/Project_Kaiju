using UnityEngine;

public class BezierPath
{
    //Calculation Values
    private Vector3 _pos0;
    private Vector3 _pos1;
    private Vector3 _pos2;
    private Vector3 _pos3;
    
    public static BezierPath CreateInstance(Transform startPosition, Transform endPosition)
    {
        return new BezierPath(startPosition, endPosition);
    }
    
    private BezierPath(Transform startPosition, Transform endPosition)
    {

        _pos0 = startPosition.position;
        _pos1 = _pos0 + startPosition.forward;
        _pos3 = endPosition.position;
        _pos2 = _pos3 - endPosition.forward; 

    }
    
    //TODO: Fuse the constructor and UpdatePath into one. Double code
    public void UpdatePath(Transform startPosition, Transform endPosition, float amplifier)
    {
        _pos0 = startPosition.position;
        _pos1 = _pos0 + startPosition.forward * amplifier;
        _pos3 = endPosition.position;
        _pos2 = _pos3 - endPosition.forward * amplifier;
    }

    public Vector3 ReturnCalculatedPosition(float t)
    {
        var returnVl = Mathf.Pow(1f - t, 3f) * _pos0 + 3f *
                       Mathf.Pow(1f - t, 2f) * t * _pos1 + 3f * (1f - t) *
                       Mathf.Pow(t, 2f) * _pos2 +
                       Mathf.Pow(t, 3f) * _pos3;
        return returnVl;
    }

    public float CalculateDistance(float begin, float destination)
    {
        var distance = Vector3.Distance(ReturnCalculatedPosition(begin), ReturnCalculatedPosition(destination));
        return distance;
    }
}
