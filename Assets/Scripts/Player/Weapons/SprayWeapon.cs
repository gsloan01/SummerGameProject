using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayWeapon : PlayerWeapon
{
    public float shrapnelAmount = 5;

    public override void Shoot()
    {
        if (cooldownTimer <= 0)
        {
            if (magCount > 0)
            {
                for (int i = 0; i < shrapnelAmount; i++)
                {
                    SpawnProjectile();
                }
                if (muzzleFlash != null) Destroy(Instantiate(muzzleFlash, projectileSpawn.position, Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward)), 0.1f);
                magCount--;
                Debug.Log("Shoot: " + magCount);
                cooldownTimer = cooldown;
            }
        }
    }
}
