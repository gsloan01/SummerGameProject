using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colliding with " + other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("WINNNNN");
            MenuController.Instance.OnLoseScreen();
        }
    }
}
