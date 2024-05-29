using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : MonoBehaviour
{
    [HideInInspector] public bool IsJumping = false;
    [SerializeField] private float _JumpBoostSpeed;
    private PlayerController _playercontroller;
    private float _timer;

    private void Start()
    {
        _playercontroller = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        // check if player has just used the booster so it doesnt double boost
        if (IsJumping)
        {
            _timer += Time.deltaTime;

            if (_timer > 0.0f)
            {
                IsJumping = false;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && !IsJumping)
        {
            IsJumping = true;
            Vector3 jump = new Vector3(0, 30, 0);
            _playercontroller._rb.AddForce(jump * _JumpBoostSpeed);
        }
    }
}
