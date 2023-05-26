using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SteerInput : MonoBehaviour
{
    [SerializeField]
    private GameObject steeringwheel;

    private PlayerInput playerInput;
    private InputAction steerAction;
    private Vector2 inputVector2;

    public float minimum;
    public float maximum;
    static float t = 0.0f;
    private float steering;
    public float steered;

    public bool hasBeenCalled = false;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        steerAction = playerInput.actions["Steer"];
    }

    private void Update()
    {
        inputVector2 = steerAction.ReadValue<Vector2>();
        
            switch (inputVector2.x)
            {
                case 0f:
                    StoppedSteering(steered);
                    break;

                case 1f:
                    SteeringRight(steered);
                    break;

                case -1f:
                    SteeringLeft(steered);
                    break;
            }
        
        Debug.Log(steered);
        if(steeringwheel != null)
        {
            // Rotates the Steering wheel
           // steeringwheel.transform.rotation = Quaternion.Euler(-21f, 102.93f, steered * 360);
        }

    }

    private void SteeringLeft(float s)
    {
        t = 0f;
        Debug.Log("Left");
        minimum = s;
        maximum = -1f;

        t += 2f * Time.deltaTime;
        steering = Mathf.Lerp(minimum, maximum, t);

        steered = steering;

        if (steered < -1f)
        {
            steered = -1f;
        }
        
    }

    private void SteeringRight(float s)
    {
        t = 0f;
        Debug.Log("Right");
        minimum = s;
        maximum = 1f;

        t += 2f * Time.deltaTime;
        steering = Mathf.Lerp(minimum, maximum, t);

        steered = steering;
        if (steered > 1f)
        {
            steered = 1f;
        }
    }

    private void StoppedSteering(float s) 
    {
        t = 0f;
        Debug.Log("Center");
        minimum = s;
        maximum = 0f;

        t += 2f * Time.deltaTime;
        steering = Mathf.Lerp(minimum, maximum, t);
        steered = steering;
        if (steered < -1f)
        {
            steered = -1f;
        }
        else if (steered > 1f)
        {
            steered = 1f;
        }

    }
}
