using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : MonoBehaviour
{
    [SerializeField] private float torpedoSpeed;
    [SerializeField] private GameObject particles;
    private GameObject toParent;

    void Start()
    {
        toParent = GameObject.Find("ParticlesAfterDestroy");
    }

    void Update()
    {
        transform.Translate(new Vector3(0, torpedoSpeed * Time.deltaTime, 0));
        if (transform.position.z > 50)
        {
            particles.transform.parent = toParent.transform;
            particles.gameObject.GetComponent<ParticleSystem>().Stop();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Boss bossHealth = collision.gameObject.GetComponent<Boss>();
        if (bossHealth != null)
        {
            bossHealth.BossTakeDamage(1);
        }

        SwarmersHealth swarmershealth = collision.gameObject.GetComponent<SwarmersHealth>();
        if (swarmershealth != null)
        {
            swarmershealth.SwarmersTakeDamage(5);
        }

        TrackerEnemyHealth trackerenemyhealth = collision.gameObject.GetComponent<TrackerEnemyHealth>();
        if (trackerenemyhealth != null)
        {
            trackerenemyhealth.TrackerEnemyTakeDamage(5);
        }

        StopShootHealth stopShootHealth = collision.gameObject.GetComponent<StopShootHealth>();
        if (stopShootHealth != null)
        {
            stopShootHealth.StopShootEnemyTakeDamage(5);
        }

        ExplosionMineHealth explosionminehealth = collision.gameObject.GetComponent<ExplosionMineHealth>();
        if (explosionminehealth != null)
        {
            explosionminehealth.MineTakeDamage(12);
        }
        HopliteHealth hoplitehealth = collision.gameObject.GetComponent<HopliteHealth>();
        if (hoplitehealth != null)
        {
            hoplitehealth.HopliteEnemyTakeDamage(5);
        }

        Destroy(gameObject);

    }
}
