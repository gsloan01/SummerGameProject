using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Movement movement;
    public float walkSpeed = 2.5f;
    public float sprintSpeed = 5.0f;

    void Start()
    {
        
    }

    void Update()
    {
        //Check sprinting and set speed accordingly
        float currentSpeed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) currentSpeed = sprintSpeed;

        //Movement controls
        Vector3 direction = Vector3.zero;
        direction.x += Input.GetAxis("Horizontal") * currentSpeed;
        direction.y = 1;
        direction.z += Input.GetAxis("Vertical") * currentSpeed;

        if (movement != null)
        {
            movement.speedMax = currentSpeed;
            movement.MoveTowards(direction);
            if (direction == Vector3.zero) movement.Stop();
        }
    }
}
