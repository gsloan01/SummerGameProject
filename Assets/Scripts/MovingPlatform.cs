using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Transform playerParent;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " entered trigger");
        if(other.gameObject.CompareTag("Player"))
        {
            playerParent = other.transform.parent;
            other.transform.parent = transform;
            Debug.Log("Player on platform");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = playerParent;
            Debug.Log("Player left platform");
        }
    }
}
