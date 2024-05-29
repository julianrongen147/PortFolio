using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingSpot : MonoBehaviour
{
    CarControl carControl;
    CarSpawn carSpawn;
    ScoreSystem scoreSystem;
    int parkingSpotIndex;

    private bool lockParkingSpot1 = false;

    private Renderer renderer;

    private bool playerInSpot = false;

    [SerializeField] private AudioSource parkingSpotChime;
    [SerializeField] private AudioClip parkingSpotChimeClip;

    private void Start()
    {
        carControl = FindObjectOfType<CarControl>();
        carSpawn = FindObjectOfType<CarSpawn>();
        scoreSystem = FindObjectOfType<ScoreSystem>();
        parkingSpotIndex = transform.GetSiblingIndex();

        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (playerInSpot && !lockParkingSpot1)
        {
            scoreSystem.playerScore += 1;
            parkingSpotChime.PlayOneShot(parkingSpotChimeClip);
            lockParkingSpot1 = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && carControl.rb.velocity.magnitude < 0.5f)
        {

            if (!lockParkingSpot1)
            {
                scoreSystem.ChangeImageColor(true, parkingSpotIndex);
                ChangeParkingSpotColor(Color.red);
                playerInSpot = true;
            }

            carSpawn.FreezePlayerInputParking();
        }
    }

    private void ChangeParkingSpotColor(Color color)
    {
        if (renderer != null)
        {
            renderer.material.color = color;
        }
    }
}
