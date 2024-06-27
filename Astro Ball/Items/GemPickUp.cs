using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GemPickUp : MonoBehaviour
{
    private GemCounter _gemCounter;
    public int Value;

    private void Start()
    {
        _gemCounter = FindObjectOfType<GemCounter>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            _gemCounter.IncreaseGems(Value);
        }
    }
}
