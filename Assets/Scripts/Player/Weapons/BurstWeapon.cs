using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstWeapon : PlayerWeapon
{
    public int roundsPerBurst = 3;
    public float timeBetweenBullets = 0.5f;

    bool isShooting = false;
    int roundsShot = 0;
    float shotTimer = 0;

    void Update()
    {
        if (isShooting)
        {
            
            if (magCount <= 0 || roundsShot >= roundsPerBurst) isShooting = false;
            else
            {
                shotTimer -= Time.deltaTime;
                if (shotTimer <= 0)
                {
                    SpawnProjectile();
                    magCount--;
                    shotTimer = timeBetweenBullets;
                    roundsShot++;
                    Debug.Log("Shoot: " + magCount);
                }
            }
        }
        else
        {
            cooldownTimer -= Time.deltaTime;
        }

    }

    public override void Shoot()
    {
        if (cooldownTimer <= 0)
        {
            roundsShot = 0;
            shotTimer = 0;
            isShooting = true;
            cooldownTimer = cooldown;
        }
    }
}
