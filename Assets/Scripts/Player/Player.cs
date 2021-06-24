using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed = 2.5f;
    public float sprintSpeed = 5.0f;
    public float drag = 0.95f;
    public float maxTiltAngle = 45.0f;
    public float tiltSpeed = 1.0f;
    
    Vector3 velocity = Vector3.zero;
    float tiltAngle = 0;

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
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        if (direction != Vector3.zero)
        {
            velocity = transform.TransformDirection(direction) * currentSpeed;
        }
        velocity *= drag;

        transform.position += velocity * Time.deltaTime;


        //Camera tilt
        float tiltDirection = direction.normalized.x;
        if (tiltDirection != 0)
        {
            tiltAngle -= tiltDirection * tiltSpeed * Time.deltaTime;
            tiltAngle = Mathf.Clamp(tiltAngle, -maxTiltAngle, maxTiltAngle);
            GetComponentInChildren<CameraControls>().Tilt('z', tiltAngle);
        }
        else
        {
            if (tiltAngle <= -0.1f || tiltAngle >= 0.1f)
            {
                tiltAngle -= tiltAngle * tiltSpeed * Time.deltaTime;
                GetComponentInChildren<CameraControls>().Tilt('z', tiltAngle);
            }
            else
            {
                tiltAngle = 0;
                GetComponentInChildren<CameraControls>().ResetTilt('z');
            }
        }
    }
}
