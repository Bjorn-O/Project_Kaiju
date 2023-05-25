using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RailSteering : MonoBehaviour
{
    [Range(1f, 25f)]
    [SerializeField] private float steeringSpeed;
    [Range(0.5f, 10f)]
    [SerializeField] private float maxDistance;

    private Transform _transform;
    private Vector3 targetPosition; 

    private float _steerDir;
    private float _targetX;

    private void Awake()
    {
        _transform = transform;
    }

    private void OnDrift(InputValue value)
    {
        _steerDir = value.Get<float>();
        print(_steerDir);
    }

    private void Update()
    {
        _targetX += _steerDir * steeringSpeed * Time.deltaTime;
        _targetX = Mathf.Clamp(_targetX, -maxDistance, maxDistance);

        _transform.localPosition = new Vector3(_targetX, 0, 0);
    }
}
