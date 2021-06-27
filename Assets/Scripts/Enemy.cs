using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioClip death;
    public float viewDistance = 35.0f;
    public Transform eyeLocator;
    //[Range(0, 360)] public float viewAngle = 90;
    public float projectileSpeed = 20;
    public GameObject projectile;
    public float health = 1;


    public enum WeaponType
    {
        Pistol, Burst, Sniper, Shotgun
    }

    public WeaponType weapon = WeaponType.Pistol;

    bool playerSighted;

    float shootTimer = 0;
    public float shootInterval = 1;

    public void Hurt(float damage)
    {
        health -= damage;
        //hurt notification
        Debug.Log("Enemy has been hurt!");
        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //delete this
        Debug.Log("Enemy died!");
        Destroy(this.gameObject);
        //make death sound
        //spawn blood effect
        //spawn ragdoll
    }

    private void Update()
    {

        //look for player
        LookForPlayer();
        if(playerSighted)
        {
            shootTimer += Time.deltaTime;
            if(shootTimer >= shootInterval)
            {
                Shoot();
            }
        }
        else
        {
            shootTimer = 0.0f;
        }
        //if player is found
            //shoot player on an interval
    }

    void Shoot()
    {
        shootTimer = 0;
        //create a bullet that travels in the direction of the player
        GameObject bullet = Instantiate(projectile, transform, false);
        Vector3 direction = GameController.Instance.player.transform.position - transform.position;
        bullet.GetComponent<Rigidbody>().AddForce((direction * projectileSpeed), ForceMode.Impulse);
    }

    void LookForPlayer()
    {
        
        //if player is within a certain distance 
        if(Vector3.Distance(transform.position, GameController.Instance.player.transform.position) <= viewDistance)
        {
            RaycastHit hit;
            Ray ray = new Ray(eyeLocator.position, (GameController.Instance.player.transform.position - eyeLocator.position));
            Debug.DrawRay(eyeLocator.position, GameController.Instance.player.transform.position - eyeLocator.position);
            //if raycast hit anything
            if (Physics.Raycast(eyeLocator.position, ray.direction, out hit))
            {
                //see what im hitting
                Debug.Log("Hit " + hit.collider.name);
                //Debug.DrawRay(eyeLocator.position, GameController.Instance.player.transform.position, Color.white);
                //hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
                //if i hit the player, set sighted to true, otherwise, false
                if (hit.collider.tag == "Player")
                {
                    playerSighted = true;
                    Debug.Log("Hit");
                }
                else 
                {
                    playerSighted = false;
                }

            }


        }

    }
}
