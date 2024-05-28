using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopShootHealth : MonoBehaviour
{
    PlayerExp playerexp;
    public StopShootHealth stopshoothealth;
    [SerializeField] private int maxHealth;
    [SerializeField] public int currentHealthStopShoot;


    void Start()
    {
        playerexp = GetComponent<PlayerExp>();
        currentHealthStopShoot = maxHealth;
    }


    public void StopShootEnemyTakeDamage(int amount)
    {
        currentHealthStopShoot -= amount;

        if (currentHealthStopShoot <= 0)
        {
            ScoreManager.instance.AddPoint(10);
            Destroy(gameObject);
        }
    }
}
