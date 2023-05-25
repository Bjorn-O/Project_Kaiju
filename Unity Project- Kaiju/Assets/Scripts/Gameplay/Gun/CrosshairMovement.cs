using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Cursor = UnityEngine.Cursor;
using Image = UnityEngine.UI.Image;

public class CrosshairMovement : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private new Camera camera;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image[] crosshairArray;
    
    [Header("Camera Controls")]
    [Range(0.1f, 2.0f)]
    [SerializeField] private float sensitivity = 1;
    [SerializeField] private float turnSpeed = 10;

    [Header("Clamping values")]
    [SerializeField] private int crosshairXClamp = 500;
    [SerializeField] private int crosshairYClamp = 500;
    [SerializeField] private int downTilt = 100;
    [SerializeField] private int rotationTarget = 50;
    
    //Private Variables
    private int _xClamp;
    private int _yClamp;
    private int _xClampMin;
    private int _yClampMin;
    private float camDir;

    private Quaternion _originalRotation;
    private Quaternion targetRotation;
    private Vector3 _desiredLocation;
    
    private void Awake()
    {
        _xClampMin = -crosshairXClamp;
        _yClampMin = -downTilt;
        _xClamp = crosshairXClamp;
        _yClamp = crosshairYClamp;
        _desiredLocation.x = 0;
        _desiredLocation.y = 0;
        
        _originalRotation = cameraPivot.localRotation;
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
    }

    private void OnRotate(InputValue value)
    {
        camDir = value.Get<float>();
    }

    private void Update()
    {
        _desiredLocation.x = Mathf.Clamp(_desiredLocation.x, _xClampMin, _xClamp);
        _desiredLocation.y = Mathf.Clamp(_desiredLocation.y, _yClampMin, _yClamp);

        foreach (var crosshair in crosshairArray)
        {
            crosshair.rectTransform.localPosition = _desiredLocation;
        }
        RotateCamera(camDir);
    }

    private void RotateCamera(float rotateAmount)
    {
        targetRotation = _originalRotation * Quaternion.Euler(0f, 0f, rotateAmount * rotationTarget);
        cameraPivot.localRotation = Quaternion.Lerp(cameraPivot.localRotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    public Vector3 DesiredLocation
    {
        get => _desiredLocation;
        set => _desiredLocation = value;
    }
}
