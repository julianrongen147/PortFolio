using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] private AudioSource _CoinPickUp;
    [SerializeField] private AudioClip _CoinPickUpEffect;
    [SerializeField] private GameObject _Coin;
    public int Value;

    private void Start()
    {
        _Coin.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _CoinPickUp.PlayOneShot(_CoinPickUpEffect);
            _Coin.gameObject.SetActive(false);
            Destroy(gameObject, 1f);
            CoinCounter.Instance.IncreaseCoins(Value);
        }
    }
}
