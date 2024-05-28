using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZigZag : MonoBehaviour
{
    public bool isPlayerMovingRight = true;
    public float strafeSpeed;
    public float forwardThrust;
    public float boundary;

    private bool locked;
    void Start()
    {

    }


    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * forwardThrust);

        if (transform.position.x > boundary)
        {
            isPlayerMovingRight = false;
        }
        if (transform.position.x < -boundary)
        {
            isPlayerMovingRight = true;
        }

        if (isPlayerMovingRight == true)
        {
            transform.Translate(Vector3.right * Time.deltaTime * strafeSpeed);
        }
        if (isPlayerMovingRight == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * strafeSpeed);
        }

    }
}
