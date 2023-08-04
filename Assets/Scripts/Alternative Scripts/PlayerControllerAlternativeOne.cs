using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerAlternativeOne : MonoBehaviour
{
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider backLeft;

    // This is to get the wheels to move, the physical wheels, only used if the car actually has separated wheels.
    [SerializeField] Transform frontRightTransform;
    [SerializeField] Transform frontLeftTransform;
    [SerializeField] Transform backRightTransform;
    [SerializeField] Transform backLeftTransform;

    private float horizontalInput;
    private float verticalInput;
    private float steeringAngle;

    public float maxSteeringAngle = 30f;
    public float motorForce = 100000f;



    //public float breakingforce = 300f;
    //private float currentBreakForce = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();

        // Update wheel meshes.
        UpdateWheelPosition(frontRight, frontRightTransform);
        UpdateWheelPosition(frontLeft, frontLeftTransform);
        UpdateWheelPosition(backRight, backRightTransform);
        UpdateWheelPosition(backLeft, backLeftTransform);
    }

    void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    void Steer()
    {
        // Take care of the steering.
        steeringAngle = maxSteeringAngle * horizontalInput;
        frontRight.steerAngle = steeringAngle;
        frontLeft.steerAngle = steeringAngle;
    }

    void Accelerate()
    {
        // Get foward/reverse acceleration from the vertical axis (W and S)
        frontRight.motorTorque = verticalInput * motorForce;
        frontLeft.motorTorque = verticalInput * motorForce;

        // Apply acceleration to back wheels.
        //backRight.motorTorque = verticalInput * motorForce;
        //backLeft.motorTorque = verticalInput * motorForce;
    }

    // Also for rotating and making the wheels turn.
    void UpdateWheelPosition(WheelCollider collider, Transform transform)
    {
        // Get wheel collider state.
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        // Set wheel transform state.
        transform.position = position;
        transform.rotation = rotation;
    }

    //void HandBrake()
    //{
    // Apply breaking force to the four wheels.
    //frontRight.brakeTorque = currentBreakForce;
    //frontLeft.brakeTorque = currentBreakForce;
    //backRight.brakeTorque = currentBreakForce;
    //backLeft.brakeTorque = currentBreakForce;    


    // If we're pressing Space, give currentBreakForce a value.
    //if (Input.GetKey(KeyCode.Space))
    //{
    //    currentBreakForce = breakingforce;
    //}
    //else
    //{
    //    currentBreakForce = 0f;
    //}
    //}
}

