using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.VirtualTexturing;

public class ShipMovement : MonoBehaviour
{
    //Compontents 
    [SerializeField] SteerInput steerInput;
    [SerializeField] protected Rigidbody m_Rigidbody;
    [SerializeField] protected Quaternion m_Rotation;

    //Input
    [SerializeField] 
    PlayerInput playerInput;
    private InputAction accelAction;
    private InputAction brakeAction;
    private InputAction holdAction;
    private InputAction steerAction;

    //Properties
    public float steerPower = 500f;
    public float power = 5f;
    public float maxSpeed = 10f;
    public float drag = 0.1f;
    public Transform pivotPoint;
    float steer;
    Vector3 forward;

    // Start is called before the first frame update
    void Awake()
    {
        //Get Compontents
        playerInput = GetComponent<PlayerInput>();
        steerInput = GetComponent<SteerInput>();
        m_Rigidbody = GetComponent<Rigidbody>();

        //Assign Input Actions
        accelAction = playerInput.actions["Accelerate"];
        brakeAction = playerInput.actions["Brake"];
        holdAction = playerInput.actions["Hold Steer"];
        steerAction = playerInput.actions["Steer"];

        m_Rotation = pivotPoint.localRotation;

    }

    // Update is called once per frame
    void Update()
    {
        //steer = steerInput.steered;
        steer = steerAction.ReadValue<Vector2>().x;
        forward = transform.rotation * Vector3.forward;

        var forceDirection = transform.forward;

        //Rotates
        m_Rigidbody.AddForceAtPosition(-steer * transform.right * steerPower / 100f * accelAction.ReadValue<float>(), pivotPoint.position);

        if (accelAction.ReadValue<float>() == 1)
        {
           ApplyForceToReachVelocity(m_Rigidbody, forward * maxSpeed, power);
        }
        else if (brakeAction.ReadValue<float>() == 1)
        {
            ApplyForceToReachVelocity(m_Rigidbody, forward * -10, power);
        }

        forward = pivotPoint.transform.rotation * Vector3.forward;

       // pivotPoint.SetPositionAndRotation(pivotPoint.position, transform.rotation * m_Rotation * Quaternion.Euler(0f, 30f * -steer,0f));

    }





    public static void ApplyForceToReachVelocity(Rigidbody rigidbody, Vector3 velocity, float force = 1, ForceMode mode = ForceMode.Force)
    {
        if (force == 0 || velocity.magnitude == 0)
            return;

        velocity = velocity + velocity.normalized * 0.2f * rigidbody.drag;

        //force = 1 => need 1 s to reach velocity (if mass is 1) => force can be max 1 / Time.fixedDeltaTime
        force = Mathf.Clamp(force, -rigidbody.mass / Time.fixedDeltaTime, rigidbody.mass / Time.fixedDeltaTime);

        //dot product is a projection from rhs to lhs with a length of result / lhs.magnitude https://www.youtube.com/watch?v=h0NJK4mEIJU
        if (rigidbody.velocity.magnitude == 0)
        {
            rigidbody.AddForce(velocity * force, mode);
        }
        else
        {
            var velocityProjectedToTarget = (velocity.normalized * Vector3.Dot(velocity, rigidbody.velocity) / velocity.magnitude);
            rigidbody.AddForce((velocity - velocityProjectedToTarget) * force, mode);
        }

    }

}
