using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDestroyBox : MonoBehaviour
{
    [SerializeField] private GameObject _DestroyBox;
    private TutorialPlayerController _player;

    private void Start()
    {
        _player = FindObjectOfType<TutorialPlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player does a groundslam attack by checking the velocity
        if (collision.gameObject.CompareTag("Player") &&
            _player._rb.velocity.y > 5)
        {
            // Instatiate another box that breaks
            Instantiate(_DestroyBox, transform.position, transform.rotation);
            if (!_player._endTutorial)
            {
                _player._endTutorial5 = true;
            }
            Debug.Log(_player._rb.velocity.y > 5);
            // Destroy the original box
            Destroy(gameObject);
        }
    }
}
