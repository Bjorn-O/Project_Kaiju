using System.Collections;
using Systems.Waypoints;
using UnityEngine;

public class RailMovement : MonoBehaviour
{
    [Range(1f, 5f)]
    [SerializeField] private float originalSpeed;
    [SerializeField] private float easeInTolerance = 0.1f;
    [Range(0.1f, 1.0f)]
    [SerializeField] private float minimumSpeedPercentage;

    private Waypoint _currentWaypoint;
    private AudioManager audioManager;
    private bool _updatingWaypoint;
    private bool _continue = true;
    private float _speed;
    private float _currentProgress;
    private float _calculationSpeed;

    private void Start()
    {
        _continue = true;
        _currentWaypoint = WayPointManager.Instance.GetWaypoint();
        audioManager = FindObjectOfType<AudioManager>();
        _currentProgress = 0;
        SetSpeed(originalSpeed);
    }

    private async void UpdateWaypoint()
    {
        _updatingWaypoint = true;
        var wayPoint = WayPointManager.Instance.GetWaypoint(_currentWaypoint);
        wayPoint.arrivalEvent?.Invoke();
        if (!WayPointManager.Instance.RequestToContinue(wayPoint))
        {
            _continue = false;
            _updatingWaypoint = false;
            return;
        }
        _currentProgress = 0;

        switch (wayPoint)
        {
            case Brakepoint:
                audioManager.Play("BoatAccelStop");
                StartCoroutine(EaseOut());
                break;

            case Splitpoint splitPoint:
                wayPoint = splitPoint.ReturnIntendedPoint();
                break;

            case WaitPoint waitPoint:
                await waitPoint.WaitForGreenLight();
                audioManager.Play("BoatAccelStart");
                StartCoroutine(EaseIn());
                break;

            default:
                if (_speed < originalSpeed)
                {
                    audioManager.Play("BoatAccelStart");
                    StartCoroutine(EaseIn());
                }
                break;
        }
        SetWaypoint(wayPoint);
        _updatingWaypoint = false;
    }

    private void Update()
    {
        if (!_continue) return;
        if (_currentWaypoint && _currentProgress <= 1f && !_updatingWaypoint)
        {
            transform.position = _currentWaypoint.GetPath().ReturnCalculatedPosition(_currentProgress);
            var oldProgress = _currentProgress;
            var distance = _currentWaypoint.GetPath().CalculateDistance(oldProgress, _currentProgress) + 0.1f;
            var step = _speed / distance * Time.deltaTime;

            _currentProgress += step;
            transform.LookAt(_currentWaypoint.GetPath().ReturnCalculatedPosition(_currentProgress));
            return;
        }
        if (!_updatingWaypoint)
        {
            UpdateWaypoint();
        }
    }

    private void SetWaypoint(Waypoint point)
    {
        _currentWaypoint = point;
    }

    private IEnumerator EaseOut()
    {
        while (_currentProgress <= 1)
        {
            _speed = Mathf.Max(_calculationSpeed * minimumSpeedPercentage, _calculationSpeed * (1 - _currentProgress));
            yield return null;
        }
    }

    private IEnumerator EaseIn()
    {
        print("Early Test");
        while (_currentProgress <= 1)
        {
            print("Test");
            _speed = Mathf.Max(_calculationSpeed * minimumSpeedPercentage, _calculationSpeed * (_currentProgress + easeInTolerance));
            yield return null;
        }
    }

    private void SetSpeed(float f)
    {
        f = f / 100;
        _speed = f;
        _calculationSpeed = f;
    }
}
