using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 2.5f;
    public float sprintSpeed = 5.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check sprinting and set speed accordingly
        float currentSpeed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) currentSpeed = sprintSpeed;

        //Movement
        Vector3 movement = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            movement.z += currentSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement.z -= currentSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement.x += currentSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement.x -= currentSpeed;
        }

        transform.position += movement * Time.deltaTime;
    }
}
