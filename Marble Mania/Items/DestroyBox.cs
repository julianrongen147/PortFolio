using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBox : MonoBehaviour
{
    [SerializeField] private GameObject _DestroyBox;
    private PlayerController _playercontroller;

    private void Start()
    {
        _playercontroller = FindObjectOfType<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player does a groundslam attack by checking the velocity
        if (collision.gameObject.CompareTag("Player") && 
            _playercontroller._rb.velocity.y > 5)
        {
            // Instatiate another box that breaks
            Instantiate(_DestroyBox, transform.position, transform.rotation);
            // Destroy the original box
            Destroy(gameObject);
        }
    }
}
