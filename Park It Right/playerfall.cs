using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerfall : MonoBehaviour
{
    CarSpawn carSpawn;

    [SerializeField] AudioSource carSplash;
    [SerializeField] AudioClip carSplashClip;

    private void Start()
    {
        carSpawn = FindObjectOfType<CarSpawn>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            carSpawn.CanRespawnCar();
            carSpawn.FreezePlayerInput();
            carSpawn.RemoveCarSound();
            carSpawn.DrowningCar();
            carSplash.PlayOneShot(carSplashClip);
        }
    }
}
