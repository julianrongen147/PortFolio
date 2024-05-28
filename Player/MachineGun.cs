using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MachineGun : MonoBehaviour
{
    GunHandler gunhandler;
    public MachineGun machinegun;
    [SerializeField] private float machineGunSpeed;
    [SerializeField] AudioSource hitMarkerEffect;
    [SerializeField] AudioClip hitMarker;

    private void Start()
    {
        gunhandler = FindObjectOfType<GunHandler>();
    }

    private void Update()
    {

        transform.Translate(new Vector3(0, 0, -machineGunSpeed * Time.deltaTime));
        if (transform.position.z > 50)
        {
            Destroy(gameObject);
        }
        else if (transform.position.z < -30)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Boss bossHealth = collision.gameObject.GetComponent<Boss>();
        if(bossHealth != null)
        {
            bossHealth.BossTakeDamage(1);
        }
        SwarmersHealth swarmershealth = collision.gameObject.GetComponent<SwarmersHealth>();
        if (swarmershealth != null)
        {
            swarmershealth.SwarmersTakeDamage(1);
        }
        TrackerEnemyHealth trackerenemyhealth = collision.gameObject.GetComponent<TrackerEnemyHealth>();
        if (trackerenemyhealth != null)
        {
            trackerenemyhealth.TrackerEnemyTakeDamage(1);
        }
        StopShootHealth stopShootHealth = collision.gameObject.GetComponent<StopShootHealth>();
        if (stopShootHealth != null)
        {
            stopShootHealth.StopShootEnemyTakeDamage(1);
        }
        HopliteHealth hoplitehealth = collision.gameObject.GetComponent<HopliteHealth>();
        if (hoplitehealth != null)
        {
            hoplitehealth.HopliteEnemyTakeDamage(1);
        }


        hitMarkerEffect.PlayOneShot(hitMarker);
        Destroy(gameObject, 0.1f); //0.1 weghalen zonder hitmarker geluid.


    }


}
