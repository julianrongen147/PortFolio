using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilarMover : MonoBehaviour
{
    private float speed;
    private float deathbox;
    void Start()
    {
        deathbox = Camera.main.transform.position.z;
        speed = Random.Range(4f, 5.5f);
    }

    void Update()
    {
        if (transform.position.z < deathbox)
        {
            Destroy(gameObject);
        }
        transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime), Space.World);
    }
}
