using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class ObjectRotation : MonoBehaviour
{
    [SerializeField] private Transform rotateObject;
    [SerializeField] private float mouseSensitivity = 10f;
    [SerializeField] private float xClamp;
    [SerializeField] private float yClamp; 
 
    private Vector2 _rotateValue;
    private Vector2 _lookRotation;

    private void OnLook(InputValue value)
    {
        _rotateValue = value.Get<Vector2>();
        ApplyRotation();
    }

    private void ApplyRotation()
    {
        print(_rotateValue);
        
        _lookRotation.x = Mathf.Clamp(_lookRotation.x -=
            _rotateValue.y * mouseSensitivity * Time.deltaTime,
            -xClamp, xClamp);
        _lookRotation.y = Mathf.Clamp(_lookRotation.y +=
                _rotateValue.x * mouseSensitivity * Time.deltaTime,
            -yClamp, yClamp);
        
        rotateObject.rotation = Quaternion.Euler(_lookRotation);
    }
}
