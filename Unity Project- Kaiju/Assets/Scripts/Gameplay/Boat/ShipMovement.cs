using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.VirtualTexturing;

public class ShipMovement : MonoBehaviour
{
    //Compontents 
    [SerializeField] protected Rigidbody m_Rigidbody;
    [SerializeField] protected Quaternion m_Rotation;

    //Input
    [SerializeField] 
    PlayerInput playerInput;
    private InputAction accelAction;
    private InputAction brakeAction;
    //private InputAction holdAction;
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
        m_Rigidbody = GetComponent<Rigidbody>();

        //Assign Input Actions
        accelAction = playerInput.actions["Accelerate"];
        brakeAction = playerInput.actions["Brake"];
        steerAction = playerInput.actions["Steer"];

        m_Rotation = pivotPoint.localRotation;

    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        //steer = steerInput.steered;

        var forward = Vector3.Scale(new Vector3(1, 0, 1), transform.forward);
        //var targetVel = Vector3.zero;
        var movingForward = Vector3.Cross(transform.forward, m_Rigidbody.velocity).y < 0;
        float currentSpeed;

        steer = steerAction.ReadValue<Vector2>().x;
        //Rotates

        currentSpeed = m_Rigidbody.velocity.magnitude;

        if (currentSpeed >= 0.5f)
        m_Rigidbody.AddForceAtPosition(-steer * transform.right * steerPower / 100f, pivotPoint.position);

        //Accelerates boat
        if (accelAction.ReadValue<float>() == 1)
        { 
           ApplyForceToReachVelocity(m_Rigidbody, forward * maxSpeed, power);
        }

        //Applies Drag so Boat can brake
        if (brakeAction.ReadValue<float>() == 1)
        {
            m_Rigidbody.drag = 2.5f;
        }
        else if (brakeAction.ReadValue<float>() == 0)
        {
            m_Rigidbody.drag = 0.5f;
        }

        m_Rigidbody.velocity = Quaternion.AngleAxis(Vector3.SignedAngle(m_Rigidbody.velocity, (movingForward ? 1f : 0f) * transform.forward, Vector3.up) * drag, Vector3.up) * m_Rigidbody.velocity;

        
        currentSpeed = Vector3.Magnitude(m_Rigidbody.velocity);
        Debug.Log(currentSpeed.ToString("F0"));
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
