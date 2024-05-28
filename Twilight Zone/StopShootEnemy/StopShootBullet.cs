using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopShootBullet : MonoBehaviour
{

    private void Start()
    {
        
    }

    void Update()
    {
        if (transform.position.z > 50 || transform.position.z < -20 || transform.position.z > 30 || transform.position.z < -30)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerHealth playerhealth = collision.gameObject.GetComponentInChildren<PlayerHealth>();
        if (collision.gameObject.CompareTag("Player"))
        {
            playerhealth.PlayerTakeDamage(15);
            Destroy(gameObject);
        }
            
        if (collision.gameObject.layer == 7)
        {
            Destroy(gameObject);
        }
        
    }
}
