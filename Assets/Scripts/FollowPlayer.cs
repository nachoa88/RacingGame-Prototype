using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(0, 30, 10);

    // Start is called before the first frame update
    void Start()
    {

    }

    // We're using LateUpdate to prevent the camera to glitter while following the car that is moving with Update in the other script.
    void LateUpdate()
    {
        // Offset for the camera following the vehicle.
        transform.position = player.transform.position + offset;
    }
}
