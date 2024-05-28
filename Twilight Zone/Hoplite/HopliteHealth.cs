using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopliteHealth : MonoBehaviour
{
    public HopliteHealth hoplitehealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealthHoplite;


    void Start()
    {
        currentHealthHoplite = maxHealth;
    }


    public void HopliteEnemyTakeDamage(int amount)
    {
        currentHealthHoplite -= amount;

        if (currentHealthHoplite <= 0)
        {
            ScoreManager.instance.AddPoint(5);
            Destroy(gameObject);
        }
    }
}
