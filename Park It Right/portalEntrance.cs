using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalEntrance : MonoBehaviour
{
    [SerializeField] private Transform spawnPointExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

            rb.transform.position = spawnPointExit.transform.position;
            rb.transform.rotation = spawnPointExit.transform.rotation;
        }
    }
}
