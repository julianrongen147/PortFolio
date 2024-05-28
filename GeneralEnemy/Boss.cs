using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private int MAX_HEALTH = 200;
    [SerializeField] private int currentHealth;
    private bool isActive = false;
    void Start()
    {  

    }

    void Update()
    {
        if (isActive)
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }
    public void BossTakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            ScoreManager.instance.AddPoint(100);
            Destroy(gameObject);
        }
    }
    public void StartBossFight()
    {
        isActive = true;
    }
}
