using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CrosshairMovement : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image crosshair;
    
    [Range(0.1f, 2.0f)]
    [SerializeField] private float sensitivity;
    
    [SerializeField] private int xClamp;
    [SerializeField] private int yClamp;
    [SerializeField] private int downTilt;

    private int _xClamp;
    private int _yClamp;
    private int _xClampMin;
    private int _yClampMin;

    private Vector3 _desiredLocation;
    
    private void Awake()
    {
        var width = Screen.width;
        var height = Screen.height;
        
        _xClampMin = width / 2 - xClamp;
        _yClampMin = height / 2 - downTilt;
        _xClamp = width / 2 + xClamp;
        _yClamp = height / 2 + yClamp;
        _desiredLocation.x = width / 2;
        _desiredLocation.y = height / 2;
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
        
        _desiredLocation.x = Mathf.Clamp(_desiredLocation.x, _xClampMin, _xClamp);
        _desiredLocation.y = Mathf.Clamp(_desiredLocation.y, _yClampMin, _yClamp);
        
        crosshair.rectTransform.position = camera.ScreenToWorldPoint(_desiredLocation);
    }
}
