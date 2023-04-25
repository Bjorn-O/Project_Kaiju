using System;
using Unity.VisualScripting;
using UnityEngine;

public class LookAtCrosshair : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Transform target;

    private Transform _gun;

    private void Awake()
    {
        _gun = transform;
    }

    private void Update()
    {
        _gun.LookAt(target);
    }
}
