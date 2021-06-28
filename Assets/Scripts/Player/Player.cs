using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<PlayerWeapon> weapons;
    public Transform weaponLocator;

    public float walkSpeed = 2.5f;
    public float sprintSpeed = 5.0f;
    public float drag = 0.95f;
    public float jumpHeight = 5.0f;
    public float boostDistance = 10.0f;
    public float boostCooldownTime = 2.0f;
    public float maxTiltAngle = 45.0f;
    public float tiltSpeed = 1.0f;
    public float weaponSwapTime = 1.5f;
    public float maxHealth = 5;

    Rigidbody rb;
    Vector3 velocity = Vector3.zero;
    bool onGround = true;
    float currentHealth = 5;
    float tiltAngle = 0;
    float boostCooldownTimer = 0;
    float weaponSwapTimer = 0;
    int weaponIndex = 0;
    PlayerWeapon currentWeapon;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (weapons.Count > 0) currentWeapon = Instantiate(weapons[weaponIndex], weaponLocator.position, weaponLocator.rotation, weaponLocator);
    }

    void Update()
    {
        //Checking if player is in the air or not
        onGround = (rb.velocity.y <= 0.01f && rb.velocity.y >= -0.01f);

        //Check sprinting and set speed accordingly
        float currentSpeed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift) && onGround) currentSpeed = sprintSpeed;


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

        Debug.DrawRay(transform.position, transform.forward);

        //Jump
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

        //Weapon Swap
        weaponSwapTimer -= Time.deltaTime;
        if (weaponSwapTimer <= 0)
        {
            Vector2 mouseDelta = Input.mouseScrollDelta;
            if (mouseDelta != Vector2.zero)
            {
                Destroy(currentWeapon?.gameObject);
                if (mouseDelta.y > 0) weaponIndex++;
                else if (mouseDelta.y < 0) weaponIndex--;
                if (weapons.Count > 0) currentWeapon = Instantiate(weapons[Mathf.Abs(weaponIndex % (weapons.Count))], weaponLocator.position, weaponLocator.rotation, weaponLocator);
            }
        }

        //Shooting
        if (currentWeapon != null)
        {
            bool shooting = (currentWeapon.fullAuto) ? Input.GetMouseButton(0) : Input.GetMouseButtonDown(0);
            if (shooting) currentWeapon.Shoot();
            if (Input.GetKeyDown(KeyCode.R)) currentWeapon.Reload(); 
        }
    }

    public void Hurt(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Player shot to " + currentHealth + "/" + maxHealth);
        if(currentHealth <= 0)
        {
            //Die
            MenuController.Instance.OnLoseScreen();
        }
    }
}
