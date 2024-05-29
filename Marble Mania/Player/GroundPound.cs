using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPound : MonoBehaviour
{
    [SerializeField] private AudioSource _GroundPoundEffect; 
    private PlayerController _playerController;
    private Rigidbody _rb;
    [SerializeField] private float _DropForce;
    
    public bool DoGroundPound;

    public bool IsGroundPounding = false;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!_playerController.IsGround && _playerController.IsBox)
            {
                DoGroundPound = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (DoGroundPound && !IsGroundPounding)
        {
            GroundPoundAttack();
        }
        DoGroundPound = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y >= 0.5)
        {
            CompleteGroundPound();
        }
    }

    private void GroundPoundAttack()
    {
        IsGroundPounding = true;
        ClearForces();
        StartCoroutine("DropAndSmash");
    }

    private IEnumerator DropAndSmash()
    {
        yield return new WaitForSeconds(0);
        _rb.AddForce(Vector3.down * _DropForce * Time.deltaTime, ForceMode.Impulse);
        _GroundPoundEffect.Play();
    }

    private void CompleteGroundPound()
    {
        IsGroundPounding = false;
    }

    private void ClearForces()
    {
        _rb.velocity = Vector3.zero;
    }
}
