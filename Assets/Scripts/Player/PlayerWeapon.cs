using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Transform projectileSpawn;
    public Projectile projectile;

    public float cooldown = 1;
    public float magSize = 30;

    float magCount;
    float cooldownTimer;

    void Start()
    {
        magCount = magSize;
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public void Shoot()
    {
        if (cooldownTimer <= 0)
        {
            cooldownTimer = cooldown;
            Instantiate(projectile, projectileSpawn.position, transform.rotation);
            magCount--;
            Debug.Log("Shoot: " + magCount);
        }
    }

    public void Reload()
    {
        if (magCount < magSize)
        {
            magCount = magSize;
            Debug.Log("Reload: " + magCount);
            //play reload animation
        }
    }
}
