using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmerAmbience : MonoBehaviour
{
    private float topMax;
    private float bottomMax;
    public float smoothSpeed = 0.125f;
    void Start()
    {
        topMax = Random.Range(0.05f, 0.1f);
        bottomMax = Random.Range(-0.05f, -0.1f);
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;

        // Calculate the target position for the game object based on the bounce animation
        Vector3 targetPosition = new Vector3(currentPosition.x, Mathf.Lerp(Random.Range(0.01f, 0.4f), Random.Range(-0.01f, -0.4f), Mathf.PingPong(Time.time, 1)), currentPosition.z);

        // Smoothly move the game object towards the target position
        transform.position = Vector3.Lerp(currentPosition, targetPosition, smoothSpeed * Time.deltaTime);
    }
    private float NewRandom(float min, float max)
    {
        return Random.Range(min, max);
    }
}
