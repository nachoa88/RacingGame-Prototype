using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerControllerCube : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    public float gravityMultiplier;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Acceleration();
        Turning();
        GravityDownforce();

       
    }

    void Acceleration()
    {
        if (Input.GetKey(KeyCode.W) && IsGrounded())
        {
            // Add the force relative to something.
            // In this case is the Z (forward), and we add a new Vector3 to keep the Y value at 0, so that the car cannot go forward into the air.
            rb.AddRelativeForce(new Vector3(Vector3.forward.x, 0, Vector3.forward.z) * speed);

        }
        if (Input.GetKey(KeyCode.S) && IsGrounded())
        {
            rb.AddRelativeForce(new Vector3(Vector3.forward.x, 0, Vector3.forward.z) * -speed);
        }
        
        // Set the forward o reverse velocity force to stop when the car turns and just follow the car's direction (X value in position).
        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
        localVelocity.x = 0;
        rb.velocity = transform.TransformDirection(localVelocity);
    }

    void Turning()
    {
        if (Input.GetKey(KeyCode.D) && IsGrounded())
        {
            rb.AddTorque(Vector3.up * turnSpeed);
        }
        if (Input.GetKey(KeyCode.A) && IsGrounded())
        {
            rb.AddTorque(Vector3.up * -turnSpeed);
        }
    }

    void GravityDownforce()
    {
        // Add extra downforce.
        rb.AddForce(Vector3.down * gravityMultiplier);
    }

    bool IsGrounded()
    {
        // 1st is the position of our Ground Check object, then the radius of sphere and last the "layer name" it should look for to set true/false.
        // The return at the beggining sets the final value (true/false) of the code also to the IsGrounded method.
        return Physics.CheckSphere(groundCheck.position, 0.2f, ground);
    }
}