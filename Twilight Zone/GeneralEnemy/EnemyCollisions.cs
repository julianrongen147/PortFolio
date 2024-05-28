using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCollisions : MonoBehaviour
{

    //[SerializeField] private string[] checkForTags;
    void Start()
    {

    }

    void Update()
    {

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    for (int i = 0; i < checkForTags.Length; i++)
    //    {
    //        if (collision.gameObject.CompareTag(checkForTags[i]))
    //        {
    //            Debug.Log("name: " + collision.gameObject.name + " | tag: " + collision.gameObject.tag);
    //            Destroy(collision.gameObject);
    //            Destroy(gameObject);

    //        }
    //        else
    //        {
    //            Debug.Log("Hit object which does not do anything");
    //        }
    //    }

    //}


    //we moeten voor elke enemy verschillende damage amounts.
    //elke enemy andere tag.
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("ENEMY COLLISION HIT");
        if (collision.gameObject.CompareTag("Swarmers"))
        {
            GetComponent<PlayerHealth>().PlayerTakeDamage(10);
        }

        if (collision.gameObject.CompareTag("TrackerEnemy(Tijdelijk)"))
        {
            GetComponent<PlayerHealth>().PlayerTakeDamage(20);
        }

        if (collision.gameObject.CompareTag("StopShootEnemy(Tijdelijk)"))
        {
            GetComponent<PlayerHealth>().PlayerTakeDamage(20);
        }

        if (collision.gameObject.CompareTag("MineExplosives"))
        {
            GetComponent<PlayerHealth>().PlayerTakeDamage(100);
        }

        if (collision.gameObject.CompareTag("Eel"))
        {
            GetComponent<PlayerHealth>().PlayerTakeDamage(100);
        }

        if (collision.gameObject.CompareTag("Hoplite"))
        {
            GetComponent<PlayerHealth>().PlayerTakeDamage(15);
        }

    }
}
