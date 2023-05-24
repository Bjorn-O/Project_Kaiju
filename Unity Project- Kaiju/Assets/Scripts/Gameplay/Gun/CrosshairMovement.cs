using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using Image = UnityEngine.UI.Image;

public class CrosshairMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private new Camera camera;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image[] crosshairs;
    
    [Header("Camera Controls")]
    [Range(0.1f, 2.0f)]
    [SerializeField] private float sensitivity;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float turnTolerance;
    
    [Header("Clamping values")]
    [SerializeField] private int crosshairXClamp;
    [SerializeField] private int crosshairYClamp;
    [SerializeField] private int downTilt;
    [SerializeField] private int rotationLimit;
    
    //Private Variables
    private int _xClamp;
    private int _yClamp;
    private int _xClampMin;
    private int _yClampMin;

    private Vector3 _desiredLocation;
    
    private void Awake()
    {
        _xClampMin = -crosshairXClamp;
        _yClampMin = -downTilt;
        _xClamp = crosshairXClamp;
        _yClamp = crosshairYClamp;
        _desiredLocation.x = 0;
        _desiredLocation.y = 0;
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnMouseLook(InputValue value)
    {
        var pos = value.Get<Vector2>();
        _desiredLocation.x += pos.x * sensitivity;
        _desiredLocation.y += pos.y * sensitivity;
        _desiredLocation.z = canvas.planeDistance;

        var overflow = _desiredLocation.x + pos.x * sensitivity;
        if (overflow > _xClamp || )
    }

    private void Update()
    {
        _desiredLocation.x = Mathf.Clamp(_desiredLocation.x, _xClampMin, _xClamp);
        _desiredLocation.y = Mathf.Clamp(_desiredLocation.y, _yClampMin, _yClamp);

        foreach (var crosshair in crosshairs)
        {
            crosshair.rectTransform.localPosition = _desiredLocation;
        }
    }

    private void RotateCamera()
    {
        
    }

    public Vector3 DesiredLocation
    {
        get => _desiredLocation;
        set => _desiredLocation = value;
    }
}
