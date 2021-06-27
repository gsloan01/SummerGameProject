using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    float damage = 1;

    void Start()
    {
        Destroy(gameObject, 5);
    }



    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hitting " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit player");
            collision.gameObject.GetComponent<Player>().Hurt(damage);
        }
        if(!collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);

        }
    }
}
