using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : MonoBehaviour
{
    [SerializeField] private float _Force;
    private GameObject _player;
    private Rigidbody _rb;
    private float _timer;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = _player.transform.position - transform.position;
        _rb.velocity = new Vector3(direction.x, direction.y, direction.z).normalized * _Force;
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 5)
        {
            Destroy(gameObject);
        }
    }
}
