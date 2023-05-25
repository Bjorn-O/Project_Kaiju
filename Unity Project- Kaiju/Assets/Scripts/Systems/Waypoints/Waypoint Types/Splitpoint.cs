using UnityEngine;

public class Splitpoint : Waypoint
{
    private bool _useSecondPath = false;

    [SerializeField] private Waypoint secondaryPath;

    public void OpenSecondaryPath()
    {
        _useSecondPath = true;
    }

    public Waypoint ReturnIntendedPoint()
    {
        return _useSecondPath ? secondaryPath : this;
    }
}
