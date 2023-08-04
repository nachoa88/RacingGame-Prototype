using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerControllerArcade : MonoBehaviour
{
    [Header("Car Settings")]
    public float moveSpeed = 80f;
    public float maxSpeed = 55f;
    public float drag = 0.98f;
    public float steerAngle = 10f;
    public float traction = 2f;
    
    private Vector3 moveForce;
    
    void Start()
    {
        
    }

    void Update()
    {
        Acceleration();
        Steering();
        DragForce();
    }

    void Acceleration()
    {
        float accelerationInput = Input.GetAxis("Vertical");
        moveForce += transform.forward * moveSpeed * accelerationInput * Time.deltaTime;
        transform.position += moveForce * Time.deltaTime;
    }

    void Steering()
    {
        float steerInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerInput * moveForce.magnitude * steerAngle * Time.deltaTime);
    }

    void DragForce()
    {
        moveForce *= drag;
        moveForce = Vector3.ClampMagnitude(moveForce, maxSpeed);

        // The next two lines of code are for seeing how the car is sliding.
        //Debug.DrawRay(transform.position, moveForce.normalized * 3);
        //Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);

        // Lerp function takes the two vectors and creates a new vector, it's good option to calculate and control sliding.
        moveForce = Vector3.Lerp(moveForce.normalized, transform.forward, traction * Time.deltaTime) * moveForce.magnitude;
    }
}
