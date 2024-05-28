using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerEnemyHealth : MonoBehaviour
{
    public TrackerEnemyHealth trackerenemyhealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealthTracker;

    
    void Start()
    {
        trackerenemyhealth = this;
        currentHealthTracker = maxHealth;
    }


    public void TrackerEnemyTakeDamage(int amount)
    {
        currentHealthTracker -= amount;

        if (currentHealthTracker <= 0)
        {
            ScoreManager.instance.AddPoint(12);
            Destroy(gameObject);
        }
    }
}
