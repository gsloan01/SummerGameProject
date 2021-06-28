using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Transform projectileSpawn;
    public Projectile projectile;
    public GameObject muzzleFlash;

    public float cooldown = 1;
    public int magSize = 30;
    public float shotDeviance = 1;

    public int roundsInMag { get { return magCount; } }

    public bool fullAuto = false;

    protected int magCount;
    protected float cooldownTimer;

    void Start()
    {
        magCount = magSize;
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public virtual void Shoot()
    {
        if (cooldownTimer <= 0 && magCount > 0)
        {
            SpawnProjectile();
            if (muzzleFlash != null) Destroy(Instantiate(muzzleFlash, projectileSpawn), 0.1f);
            magCount--;
            cooldownTimer = cooldown;
            Debug.Log("Shoot: " + magCount);
        }
    }

    public void SpawnProjectile()
    {
        Quaternion projectileRotation = Quaternion.Euler(Random.insideUnitCircle * shotDeviance) * transform.rotation;
        Instantiate(projectile, projectileSpawn.position, projectileRotation);
    }

    public virtual void Reload()
    {
        if (magCount < magSize)
        {
            magCount = magSize;
            Debug.Log("Reload: " + magCount);
            //play reload animation
        }
    }
}
