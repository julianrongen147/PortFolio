using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float JumpForce;
    [SerializeField] private float speed;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        Vector3 force = new Vector3(0, JumpForce, 0);

        rb.AddForce(force, ForceMode.Impulse);

    }
}
