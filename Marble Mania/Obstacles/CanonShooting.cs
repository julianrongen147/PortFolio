using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonShooting : MonoBehaviour
{
    [SerializeField] private GameObject _Bullet;
    [SerializeField] private Transform _BulletPos;

    [SerializeField] private float _ShootTimer;
    [SerializeField] private float _RotateSpeed;

    private float timer;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, _player.transform.position);

        if (distance < 15)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - _player.transform.position), _RotateSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            if (timer > _ShootTimer)
            {
                timer = 0;
                Shoot();
            }
        }

    }

    private void Shoot()
    {
        Instantiate(_Bullet, _BulletPos.position, Quaternion.identity);
    }
}
