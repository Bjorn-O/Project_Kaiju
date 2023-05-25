using System;
using Unity.VisualScripting;
using UnityEngine;

public class LookAtCrosshair : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float rotationSpeed = 10f;

    private Transform _gun;

    private void Awake()
    {
        _gun = transform;
    }

    private void Update()
    {
        var targetDir = target.position - _gun.position;
        var targetRot = Quaternion.LookRotation(targetDir);
        
        transform.rotation = Quaternion.Slerp(_gun.rotation, targetRot, rotationSpeed * Time.deltaTime);
    }
}
