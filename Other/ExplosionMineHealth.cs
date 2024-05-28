using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionMineHealth : MonoBehaviour
{
    public ExplosionMineHealth explosionminehealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealthMine;

    void Start()
    {
        currentHealthMine = maxHealth;
    }


    public void MineTakeDamage(int amount)
    {
        currentHealthMine -= amount;

        if (currentHealthMine <= 0)
        {
            ScoreManager.instance.AddPoint(5);
            Destroy(gameObject);
        }
    }
}
