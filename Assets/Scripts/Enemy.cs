using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioClip death;
    public float viewDistance = 35.0f;
    [Range(0, 360)] public float viewAngle = 90;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("PlayerWeapon"))
        {
            Die();
        }
    }
    public void Die()
    {
        //delete this
        //make death sound
        //spawn blood effect
        //spawn ragdoll
    }

    private void Update()
    {
        //look for player
        //if player is found
            //shoot player on an interval
    }

    bool LookForPlayer()
    {
        //if player is within a certain distance && if player is within viewing angle
            //check if a raycast from this enemy to the player hits the player
            //if so, player has been found return true
        return false;
    }
}
