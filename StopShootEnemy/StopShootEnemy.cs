using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopShootEnemy : MonoBehaviour
{
    StopShootHealth stopshoolhealth;
    public GameObject bulletPrefab;
    public Transform bulletPos;
    public Transform bulletPos1;
    public Transform bulletPos2;
    public Transform bulletPos3;
    private float timer;
    [SerializeField] private float bulletSpeed;



    void Start()
    {
        stopshoolhealth = FindObjectOfType<StopShootHealth>();
    }


    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2)
        {
            timer = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (stopshoolhealth.currentHealthStopShoot > 0)
        {
            var bullet = Instantiate(bulletPrefab, bulletPos.position, bulletPos.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletPos.forward * bulletSpeed;

            var bullet1 = Instantiate(bulletPrefab, bulletPos1.position, bulletPos1.rotation);
            bullet1.GetComponent<Rigidbody>().velocity = bulletPos1.forward * bulletSpeed;

            var bullet2 = Instantiate(bulletPrefab, bulletPos2.position, bulletPos2.rotation);
            bullet2.GetComponent<Rigidbody>().velocity = bulletPos2.forward * bulletSpeed;

            var bullet3 = Instantiate(bulletPrefab, bulletPos3.position, bulletPos3.rotation);
            bullet3.GetComponent<Rigidbody>().velocity = bulletPos3.forward * bulletSpeed;
        }
    }
}
