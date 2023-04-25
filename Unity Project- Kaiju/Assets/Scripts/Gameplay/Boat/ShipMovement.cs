using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] 
    private Rigidbody shipRb;

    [SerializeField]
    FovOnAccelerate fovOnAccelerate;

    //Input booleans
    private bool _isAccelerating;
    private bool _isBraking;

    //Boat Properties
    public float steerPower = 100f;
    public float power = 1f;
    public float maxSpeed = 24f;
    public float drag = 0.1f;
    public Transform pivotPoint;
    private float _steer;

    // Start is called before the first frame update
    private void Awake()
    {
        //Get Components
        shipRb = GetComponent<Rigidbody>();
        //fovOnAccelerate = GetComponent<FovOnAccelerate>();
    }

    //Brake input
    private void OnBrake(InputValue value)
    {
        if (value.Get<float>() == 1) _isBraking = true;
        else _isBraking = false;
    }
    //Accelerate input
    private void OnAccelerate(InputValue value)
    {
        if (value.Get<float>() == 1)
        {
            _isAccelerating = true;
            fovOnAccelerate.accelerate = true;
        }
        else
        {
            _isAccelerating = false;
            fovOnAccelerate.accelerate = false;
        }
    }
    //Steering input
    private void OnSteer(InputValue value)
    {
        _steer = value.Get<Vector2>().x;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var forward = Vector3.Scale(new Vector3(1, 0, 1), transform.forward);
        var movingForward = Vector3.Cross(transform.forward, shipRb.velocity).y < 0;
        var currentSpeed = shipRb.velocity.magnitude;

        //Rotates boat by applying force on the pivot point 
        if (currentSpeed >= 0.5f)
        shipRb.AddForceAtPosition(-_steer * transform.right * steerPower / 100f, pivotPoint.position);

        //Accelerates boat
        if(_isAccelerating) ApplyForceToReachVelocity(shipRb, forward * maxSpeed, power);

        //Applies Drag so Boat can brake
        shipRb.drag = _isBraking ? 2.5f : 0.5f;

        Vector3 velocity = Quaternion.AngleAxis(Vector3.SignedAngle(shipRb.velocity, (movingForward ? 1f : 0f) * transform.forward, Vector3.up) * drag, Vector3.up) * (velocity = shipRb.velocity);
        shipRb.velocity = velocity;

        currentSpeed = Vector3.Magnitude(velocity);
        //Debug.Log(currentSpeed.ToString("F0"));
    }
    
    private void ApplyForceToReachVelocity(Rigidbody rb, Vector3 velocity, float force = 1, ForceMode mode = ForceMode.Force)
    {
        if (force == 0 || velocity.magnitude == 0) return;
    
        velocity = velocity + velocity.normalized * (0.2f * rb.drag);

        //force = 1 => need 1 s to reach velocity (if mass is 1) => force can be max 1 / Time.fixedDeltaTime
        force = Mathf.Clamp(force, -rb.mass / Time.fixedDeltaTime, rb.mass / Time.fixedDeltaTime);

        if (rb.velocity.magnitude == 0)
        {
            rb.AddForce(velocity * force, mode);
            return;
        }
        var velocityProjectedToTarget = (velocity.normalized * Vector3.Dot(velocity, rb.velocity) / velocity.magnitude); 
        rb.AddForce((velocity - velocityProjectedToTarget) * force, mode);
    }
}