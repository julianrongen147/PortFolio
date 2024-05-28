using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightLineEnemy : MonoBehaviour
{
    [SerializeField] private float speed;


    void Update()
    {
        transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));

        if (transform.position.z < -50)
        {
            Destroy(gameObject);
        }
    }
}
