using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerRotate : MonoBehaviour
{
    [SerializeField] private float _Speed;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //rb.MoveRotation(rb.rotation * Quaternion.AngleAxis(speed * Time.deltaTime, Vector3.right));
        transform.Rotate(Vector3.right, _Speed * Time.deltaTime);
    }
}
