using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStandStill : MonoBehaviour
{
    CarControl carControl;
    CarSpawn carSpawn;

    private bool isTouchingParkingSpot;

    private void Start()
    {
        carControl = GetComponent<CarControl>();
        carSpawn = FindObjectOfType<CarSpawn>();
    }

    private void Update()
    {
        if (carSpawn.carCanGetParked)
        {
            FreezePlayerWhenStandingStill();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("ParkingSpot"))
        {
            isTouchingParkingSpot = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ParkingSpot"))
        {
            isTouchingParkingSpot = false;
        }
    }


    private void FreezePlayerWhenStandingStill()
    {
        if (carControl.rb.velocity.magnitude < 0.5f && !isTouchingParkingSpot)
        {
            carSpawn.FreezePlayerInput();
            carControl.rb.velocity = Vector3.zero;
        }
    }
}
