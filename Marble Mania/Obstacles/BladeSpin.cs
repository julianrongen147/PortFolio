using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeSpin : MonoBehaviour
{
    [SerializeField] private float _Speed;
    [SerializeField] private AudioSource _BladeSpin;
    [SerializeField] private AudioClip _BladeSpinEffect;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _BladeSpin.PlayOneShot(_BladeSpinEffect);
    }

    void FixedUpdate()
    {
        _rb.MoveRotation(_rb.rotation * Quaternion.AngleAxis(_Speed * Time.deltaTime, Vector3.up));
        
    }
}
