using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFallOffMap : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private Transform _player;
    [SerializeField] private Transform _spawnPosition;
    //private Scene _scene;

    private void Start()
    {
        //_scene = SceneManager.GetActiveScene();
        rb = FindObjectOfType<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //SceneManager.LoadScene(_scene.name);
            _player.position = _spawnPosition.position;
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
}
