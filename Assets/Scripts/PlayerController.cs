using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Car Settings")]
    public float speedFactor = 50.0f;
    public float turnFactor = 5f;
    public float gravityMultiplier;

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask ground;

    public bool isMovingForward = false;

    private Rigidbody carRb;

    private float horizontalInput;
    private float currentVelocity;

    // Start is called before the first frame update
    void Start()
    {
        carRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void FixedUpdate()
    {
        ApplyEngineForce();
        ApplySteering();
        GravityDownforce();
    }

    void ApplyEngineForce()
    {
        if (Input.GetKey(KeyCode.UpArrow) && IsGrounded())
        {
            // Create a force for the engine.
            Vector3 engineForceVector = Vector3.forward * speedFactor;
            // Apply force that pushes the car forward
            carRb.AddRelativeForce(engineForceVector, ForceMode.Acceleration);
            isMovingForward = true;
        }
        if (Input.GetKey(KeyCode.DownArrow) && IsGrounded())
        {
            Vector3 engineForceVector = Vector3.forward * -speedFactor / 1.5f;
            carRb.AddRelativeForce(engineForceVector, ForceMode.Acceleration);
            isMovingForward = false;
        }
    }

    // This is to be able to work on the speedFactor in another script and add or equal to the new speed in that scritp.
    public void SetMoveSpeed(float newSpeedAdjustment)
    {
        speedFactor += newSpeedAdjustment;
        // Here we could add an effect or something when this happens.
    }

    void ApplySteering()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Take the current velocity of car & divide by max velocity (speedFactor) to have a number between 0 & 1. So then if the velocity is zero, car won't turn.
        currentVelocity = carRb.velocity.magnitude / speedFactor;

        if (isMovingForward)
        {
            // Update rotation angle based on input.
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnFactor * horizontalInput, 0f) * currentVelocity);
        }
        if (!isMovingForward)
        {
            // Update rotation angle based on input.
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, -turnFactor * horizontalInput, 0f) * currentVelocity);
        }
        
    }

    void GravityDownforce()
    {
        // Add extra downforce.
        carRb.AddForce(Vector3.down * gravityMultiplier);
    }

    bool IsGrounded()
    {
        // 1st is the position of our Ground Check object, then the radius of sphere and last the "layer name" it should look for to set true/false.
        // The return at the beggining sets the final value (true/false) of the code also to the IsGrounded method.
        return Physics.CheckSphere(groundCheck.position, 0.2f, ground);
    }
}
