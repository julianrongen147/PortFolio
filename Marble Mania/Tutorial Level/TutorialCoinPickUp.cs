using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCoinPickUp : MonoBehaviour
{
    [SerializeField] private AudioSource _CoinPickUp;
    [SerializeField] private AudioClip _CoinPickUpEffect;
    [SerializeField] private GameObject _Coin;
    private TutorialPlayerController _player;

    private void Start()
    {
        _player = FindObjectOfType<TutorialPlayerController>();
        _Coin.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!_player._endTutorial)
            {
                _player._endTutorial4 = true;
            }

            _CoinPickUp.PlayOneShot(_CoinPickUpEffect);
            _Coin.gameObject.SetActive(false);
            Destroy(gameObject, 1f);
        }
    }
}
