using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeparateWheel : MonoBehaviour
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


    public float acceleration = 500f;
    public float breakingforce = 300f;
    public float maxTurnAngle = 25f;

    private float currentAcceleration = 0f;
    private float currentBreakForce = 0f;
    private float currentTurnAngle = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Get foward/reverse acceleration from the vertical axis (W and S)
        currentAcceleration = acceleration * Input.GetAxis("Vertical");
        
        // If we're pressing Space, give currentBreakForce a value.
        if (Input.GetKey(KeyCode.Space))
        {
            currentBreakForce = breakingforce;
        }
        else
        {
            currentBreakForce = 0f;
        }

        // Apply acceleration to front wheels.
        frontRight.motorTorque = currentAcceleration;
        frontLeft.motorTorque = currentAcceleration;
        //backRight.motorTorque = currentAcceleration;
        //backLeft.motorTorque = currentAcceleration;

        // Apply breaking force to the four wheels.
        frontRight.brakeTorque = currentBreakForce;
        frontLeft.brakeTorque = currentBreakForce;
        backRight.brakeTorque = currentBreakForce;
        backLeft.brakeTorque = currentBreakForce;

        // Take care of the steering.
        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
        frontRight.steerAngle = currentTurnAngle; 
        frontLeft.steerAngle = currentTurnAngle;

        // Update wheel meshes.
        UpdateWheel(frontRight, frontRightTransform); 
        UpdateWheel(frontLeft, frontLeftTransform);
        UpdateWheel(backRight, backRightTransform);
        UpdateWheel(backLeft, backLeftTransform);
    }

    // Also for rotating and making the wheels turn.
    void UpdateWheel(WheelCollider col, Transform trans)
    {
        // Get wheel collider state.
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);

        // Set wheel transform state.
        trans.position = position;
        trans.rotation = rotation;
    }
}
