using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.Serialization;

public class ShipMovement : MonoBehaviour
{
    //Components 
    [SerializeField] 
    private Rigidbody shipRb;
    [SerializeField] 
    private Quaternion rotation;

    //Input
    [SerializeField] 
    PlayerInput playerInput;
    private InputAction _accelAction;
    private InputAction _brakeAction;
    //private InputAction holdAction;
    private InputAction _steerAction;

    //Properties
    public float steerPower = 500f;
    public float power = 5f;
    public float maxSpeed = 10f;
    public float drag = 0.1f;
    public Transform pivotPoint;
    private float _steer;
    private Vector3 _forward;


    // Start is called before the first frame update
    private void Awake()
    {
        //Get Components
        playerInput = GetComponent<PlayerInput>();
        shipRb = GetComponent<Rigidbody>();

        //Assign Input Actions
        _accelAction = playerInput.actions["Accelerate"];
        _brakeAction = playerInput.actions["Brake"];
        _steerAction = playerInput.actions["Steer"];

        rotation = pivotPoint.localRotation;

    }
    
    // Update is called once per frame
    private void FixedUpdate()
    {
        //steer = steerInput.steered;

        var forward = Vector3.Scale(new Vector3(1, 0, 1), transform.forward);
        //var targetVel = Vector3.zero;
        var movingForward = Vector3.Cross(transform.forward, shipRb.velocity).y < 0;

        _steer = _steerAction.ReadValue<Vector2>().x;

        //Rotates
        var currentSpeed = shipRb.velocity.magnitude;

        if (currentSpeed >= 0.5f)
        shipRb.AddForceAtPosition(-_steer * transform.right * steerPower / 100f, pivotPoint.position);

        //Accelerates boat
        if (Math.Abs(_accelAction.ReadValue<float>() - 1) < 0.01f) //Last number represents Tolerance. Introduce variable
        { 
           ApplyForceToReachVelocity(shipRb, forward * maxSpeed, power);
        }

        //Applies Drag so Boat can brake
        if (Math.Abs(_brakeAction.ReadValue<float>() - 1) < 0.01f) //Last number represents Tolerance. Introduce variable
        {
            shipRb.drag = 2.5f;
        }
        else if (_brakeAction.ReadValue<float>() == 0)
        {
            shipRb.drag = 0.5f;
        }

        Vector3 velocity = Quaternion.AngleAxis(Vector3.SignedAngle(shipRb.velocity, (movingForward ? 1f : 0f) * transform.forward, Vector3.up) * drag, Vector3.up) * (velocity = shipRb.velocity);
        shipRb.velocity = velocity;


        currentSpeed = Vector3.Magnitude(velocity);
        Debug.Log(currentSpeed.ToString("F0"));
    }
    
    private void ApplyForceToReachVelocity(Rigidbody rb, Vector3 velocity, float force = 1, ForceMode mode = ForceMode.Force)
    {
        if (force == 0 || velocity.magnitude == 0) return;
    
        velocity = velocity + velocity.normalized * (0.2f * rb.drag);

        //force = 1 => need 1 s to reach velocity (if mass is 1) => force can be max 1 / Time.fixedDeltaTime
        force = Mathf.Clamp(force, -rb.mass / Time.fixedDeltaTime, rb.mass / Time.fixedDeltaTime);

        //dot product is a projection from rhs to lhs with a length of result / lhs.magnitude https://www.youtube.com/watch?v=h0NJK4mEIJU
        if (rb.velocity.magnitude == 0)
        {
            rb.AddForce(velocity * force, mode);
        }
        else
        {
            var velocityProjectedToTarget = (velocity.normalized * Vector3.Dot(velocity, rb.velocity) / velocity.magnitude);
            rb.AddForce((velocity - velocityProjectedToTarget) * force, mode);
        }
    }
}