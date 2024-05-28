using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopliteAi : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));

        if (gameObject.transform.position.z < - 20)
        {
            Destroy(gameObject);
        }
    }


}
