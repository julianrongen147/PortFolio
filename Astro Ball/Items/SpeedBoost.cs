using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [HideInInspector] public bool IsMoving = false;
    [SerializeField] private float _SpeedBoost;
    private PlayerController _playercontroller;
    private float _timer;

    private void Start()
    {
        _playercontroller = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        // check if player has just used the booster so it doesnt double boost
        if (IsMoving)
        {
            _timer += Time.deltaTime;

            if (_timer > 0.2f)
            {
                IsMoving = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !IsMoving)
        {
            IsMoving = true;
            Vector3 move = new Vector3(0, 0, 20);
            _playercontroller._rb.AddForce(move * _SpeedBoost);
        }
    }

}
