using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCameraFollow : MonoBehaviour
{
    public float smoothing;
    public float turnsmoothing;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // We're using FixedUpdate because the player is using FixedUpdate.
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.position, smoothing);
        // Rotating the camera when the car rotates.
        transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, turnsmoothing);
        // Prevent the camera of rotating in the X & Z angles when the car rotates.
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
    }
}
