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
                magCount--;
                Debug.Log("Shoot: " + magCount);
                cooldownTimer = cooldown;
            }
        }
    }
}
