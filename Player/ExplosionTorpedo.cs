using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTorpedo : MonoBehaviour
{
    public GameObject collisionParticleSystem;

    private void OnCollisionEnter(Collision collision)
    {
        //MAG MAAR 1 KEER AFGAAN PER KEER, hij explodeert nu op meerdere enemies.
        if (collision.gameObject.layer == 7)
        {
            Instantiate(collisionParticleSystem, transform.position, Quaternion.identity);
            AreaDamageEnemies(transform.position, 1.5f);
        }
    }

    private void AreaDamageEnemies(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            //enemies
            Boss bossHealth = hitCollider.gameObject.GetComponent<Boss>();
            if (bossHealth != null)
            {
                bossHealth.BossTakeDamage(5);
            }
            SwarmersHealth swarmershealth = hitCollider.gameObject.GetComponent < SwarmersHealth>();
            if (swarmershealth != null)
            {
                swarmershealth.SwarmersTakeDamage(2);
            }
            TrackerEnemyHealth trackerenemyhealth = hitCollider.gameObject.GetComponent<TrackerEnemyHealth>();
            if (trackerenemyhealth != null)
            {
                trackerenemyhealth.TrackerEnemyTakeDamage(2);
            }
            StopShootHealth stopShootHealth = hitCollider.gameObject.GetComponent<StopShootHealth>();
            if (stopShootHealth != null)
            {
                stopShootHealth.StopShootEnemyTakeDamage(2);
            }

            //player
            PlayerHealth playerhealth = hitCollider.gameObject.GetComponent<PlayerHealth>();
            if (playerhealth != null)
            {
                playerhealth.PlayerTakeDamage(10);
            }
        }
    }
}
