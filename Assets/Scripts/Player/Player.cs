using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerWeapon weapon;

    public float walkSpeed = 2.5f;
    public float sprintSpeed = 5.0f;
    public float drag = 0.95f;
    public float jumpHeight = 5.0f;
    public float boostDistance = 10.0f;
    public float maxTiltAngle = 45.0f;
    public float tiltSpeed = 1.0f;
    public float boostCooldownTime = 2.0f;

    Rigidbody rb;
    Vector3 velocity = Vector3.zero;
    float tiltAngle = 0;
    float boostCooldownTimer = 0;
    bool onGround = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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


        //Jump
        onGround = (rb.velocity.y == 0);
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
        }


        //Boost
        boostCooldownTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (boostCooldownTimer <= 0)
            {
                Vector3 boostDirection = GetComponentInChildren<CameraControls>().transform.forward;
                rb.AddForce(boostDirection * boostDistance, ForceMode.VelocityChange);
                boostCooldownTimer = boostCooldownTime;
            }
        }

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

        //Shooting
        if (Input.GetMouseButton(0)) weapon?.Shoot();
        if (Input.GetKeyDown(KeyCode.R)) weapon?.Reload(); 
    }
}
