using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class ObjectRotation : MonoBehaviour
{
    public UnityEvent<Vector2> onObjectRotation;
    public LayerMask mask; 

    [SerializeField] private Transform rotateObject;
    [SerializeField] private float mouseSensitivity = 10f;
    [SerializeField] private float xClamp;
    [SerializeField] private float yClamp; 
 
    private Vector2 _rotateValue;
    private Vector2 _lookRotation;

    private RaycastHit _hit;
    
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnLook(InputValue value)
    {
        _rotateValue = value.Get<Vector2>();
        ApplyRotation();
    }
    
    private void ApplyRotation()
    {
        var xLook= _lookRotation.x - (_rotateValue.y * mouseSensitivity * Time.deltaTime);
        var yLook = _lookRotation.y + (_rotateValue.x * mouseSensitivity * Time.deltaTime);
        
        _lookRotation.x = Mathf.Clamp(xLook,-xClamp, 0);
        _lookRotation.y = Mathf.Clamp(yLook, -yClamp, yClamp);

        rotateObject.rotation = Quaternion.Euler(_lookRotation);
        onObjectRotation?.Invoke(_lookRotation);
    }
}
