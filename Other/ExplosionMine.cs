using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionMine : MonoBehaviour
{
    public GameObject collisionParticleSystem;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.name.Contains("Torpedo 1"))
        {
            Instantiate(collisionParticleSystem, transform.position, Quaternion.identity);
            AreaDamageEnemies(transform.position, 5f);
        }
    }

    private void AreaDamageEnemies(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            //enemies
            SwarmersHealth swarmershealth = hitCollider.gameObject.GetComponent<SwarmersHealth>();
            if (swarmershealth != null)
            {
                swarmershealth.SwarmersTakeDamage(100);
            }
            TrackerEnemyHealth trackerenemyhealth = hitCollider.gameObject.GetComponent<TrackerEnemyHealth>();
            if (trackerenemyhealth != null)
            {
                trackerenemyhealth.TrackerEnemyTakeDamage(100);
            }
            StopShootHealth stopShootHealth = hitCollider.gameObject.GetComponent<StopShootHealth>();
            if (stopShootHealth != null)
            {
                stopShootHealth.StopShootEnemyTakeDamage(100);
            }

            //player
            PlayerHealth playerhealth = hitCollider.gameObject.GetComponent<PlayerHealth>();
            if (playerhealth != null)
            {
                playerhealth.PlayerTakeDamage(100);
            }
        }
    }
}
