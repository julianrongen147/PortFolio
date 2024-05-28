using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmersHealth : MonoBehaviour
{
    public SwarmersHealth swarmershealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealthSwarmer;

    
    void Start()
    {
        currentHealthSwarmer = maxHealth;
    }

    
    public void SwarmersTakeDamage(int amount)
    {
        currentHealthSwarmer -= amount;

        if (currentHealthSwarmer <= 0)
        {
            ScoreManager.instance.AddPoint(1);
            Destroy(gameObject);
        }
    }
}
